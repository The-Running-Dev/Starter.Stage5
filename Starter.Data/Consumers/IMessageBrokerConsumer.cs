using Starter.Data.Entities;

namespace Starter.Data.Consumers
{
    /// <summary>
    /// Defines the contract for the message broker consumer
    /// </summary>
    public interface IMessageBrokerConsumer
    {
        void OnDataReceived(object sender, Message<Cat> message);

        bool Start();

        bool Stop();
    }
}