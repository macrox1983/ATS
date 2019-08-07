using ATS.Common;
using ATS.Common.Messages;
using ATS.MessageBus;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ATS.Tests
{
    [TestClass]
    public class MessageBusTest
    {
        [TestMethod]        
        public void TestSubscribeAndReceiveMessage()
        {
            IMessageBusClient messageBusClient = new MessageBus.MessageBusClient();
            
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    Task.Delay(new Random().Next(300));
                    messageBusClient.Subscribe<ToursRequest>(ReceiveRequest);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            messageBusClient.SendMessage(new ToursRequest(_requestId));
        }

        private Guid _requestId = Guid.NewGuid();

        private void ReceiveRequest(ToursRequest request)
        {
            Assert.AreEqual(_requestId, request.RequestId);
        }
    }
}
