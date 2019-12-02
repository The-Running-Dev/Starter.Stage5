using System;

using Microsoft.Extensions.Logging;

using Starter.Data.Entities;
using Starter.Data.Services;
using Starter.Framework.Clients;
using Starter.Framework.Extensions;

namespace Starter.Data.Consumers
{
    /// <summary>
    /// Implements the message broker consumer
    /// </summary>
    public class MessageBrokerConsumer : IMessageBrokerConsumer
    {
        private readonly IMessageBroker<Cat> _messageBroker;
        
        private readonly IApiClient _apiClient;
        
        private readonly ILogger _logger;

        public MessageBrokerConsumer(IMessageBroker<Cat> messageBroker, IApiClient apiClient, ILogger logger)
        {
            _messageBroker = messageBroker;
            _apiClient = apiClient;
            _logger = logger;

            _messageBroker.DataReceived += OnDataReceived;
        }

        /// <summary>
        /// Handles the data received from the message broker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        public void OnDataReceived(object sender, Message<Cat> message)
        {
            _logger.Log(LogLevel.Information, $"{message.Command}, {message.Type}, {message.Entity.ToJson()}");

            switch (message.Command)
            {
                case MessageCommand.Create:
                    _apiClient.Create(message.Entity);

                    break;
                case MessageCommand.Update:
                    _apiClient.Update(message.Entity);

                    break;
                case MessageCommand.Delete:
                    _apiClient.Delete(message.Entity.Id);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }   

        public bool Start()
        {
            _messageBroker.Receive();

            return true;
        }

        public bool Stop()
        {
            _messageBroker.Stop();
            
            return true;
        }
    }
}