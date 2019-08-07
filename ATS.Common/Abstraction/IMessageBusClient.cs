using ATS.Common.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Common
{
    public interface IMessageBusClient
    {
        void Subscribe<TMessage>(Action<TMessage> callback) where TMessage : class, IMessage;        

        int GetSubscriberCount<TMessage>() where TMessage : class, IMessage;

        int GetSubscriberCount(Type messageType);

        void SendMessage<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}
