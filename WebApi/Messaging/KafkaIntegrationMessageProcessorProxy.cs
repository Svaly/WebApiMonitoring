using System;
using System.Collections.Generic;
using Framework.Messaging.Kafka.Consume;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace WebApi.Messaging
{
    public class KafkaIntegrationMessageProcessorProxy : IKafkaConsumedMessageProcessor
    {
        private readonly Container _container;
        private readonly Func<IKafkaConsumedMessageProcessor> _kafkaConsumedMessageProcessor;

        public KafkaIntegrationMessageProcessorProxy(
            Container container,
            Func<IKafkaConsumedMessageProcessor> kafkaConsumedMessageProcessor)
        {
            _container = container;
            _kafkaConsumedMessageProcessor = kafkaConsumedMessageProcessor;
        }

        public void ProcessMessage(KeyValuePair<string, string> message, string connectionName)
        {
            using (AsyncScopedLifestyle.BeginScope(_container))
            {
                _kafkaConsumedMessageProcessor().ProcessMessage(message, connectionName);
            }
        }
    }
}