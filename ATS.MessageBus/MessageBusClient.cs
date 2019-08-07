using ATS.Common;
using ATS.Common.Messages;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATS.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        private ConcurrentDictionary<Type, ConcurrentBag<Action<object>>> _subscribers;

        public MessageBusClient()
        {
            _subscribers = new ConcurrentDictionary<Type, ConcurrentBag<Action<object>>>();
        }
        
        public void Subscribe<TMessage>(Action<TMessage> callback) where TMessage:class, IMessage
        {
            ConcurrentBag<Action<object>> subscribers = null;
            if (_subscribers.TryGetValue(typeof(TMessage), out subscribers) && subscribers != null)
            {
                    var newSubscribers = new ConcurrentBag<Action<object>>(subscribers);
                    newSubscribers.Add(o => callback((TMessage)o));
                    _subscribers.TryUpdate(typeof(TMessage), newSubscribers, subscribers);
            }
            else
            {
                _subscribers.TryAdd(typeof(TMessage), new ConcurrentBag<Action<object>>() { o => callback((TMessage)o) });
            }            
        }

        /// <summary>
        /// Отправляем сообщение всем подписчикам
        /// </summary>
        /// <typeparam name="TMessage"> тип сообщения</typeparam>
        /// <param name="message"> сообщение</param>
        /// <returns></returns>
        public void SendMessage<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            ConcurrentBag<Action<object>> subscribers;

            if(_subscribers.TryGetValue(message.GetType(), out subscribers) && subscribers != null)
            {
                Parallel.ForEach(subscribers.ToArray(), s=>s(message));                           
            }
        }

        public int GetSubscriberCount<TMessage>() where TMessage : class, IMessage
        {
            return GetSubscriberCount(typeof(TMessage));
        }

        public int GetSubscriberCount(Type messageType)
        {
            ConcurrentBag<Action<object>> subscribers;

            if (_subscribers.TryGetValue(messageType, out subscribers) && subscribers != null)
            {
                return subscribers.Count;
            }
            return 0;
        }
    }
}
