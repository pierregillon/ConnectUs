using System.Collections.Generic;

namespace ConnectUs.Business.Tests.Mocks
{
    public class FakeConnection : IConnection
    {
        private readonly Queue<Message> _messages = new Queue<Message>();

        public void Send<T>(T message) where T : Message
        {
            _messages.Enqueue(message);
        }

        public T Read<T>() where T : Message
        {
            return (T) _messages.Dequeue();
        }
    }
}