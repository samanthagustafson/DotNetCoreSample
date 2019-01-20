using System;
using Confluent.Kafka;
using MongoDB.Bson;

namespace MoveToCore.Loggers
{
    public class KafkaLogger : ILogger
    {
        private readonly ProducerConfig _producerConfig;

        public KafkaLogger()
        {
            _producerConfig = new ProducerConfig
            {
                GroupId = "Movetocore",
                SessionTimeoutMs = 10000,
                BootstrapServers = Environment.GetEnvironmentVariable("KAFKA_HOST")
            };
        }

        public async void Write(string log)
        {
            log = new
            {
                message = log
            }.ToJson();

            using (var kafkaProducer = new Producer<Null, string>(_producerConfig))
            {
                await kafkaProducer.ProduceAsync("MovetocoreLogs", new Message<Null, string> {Value = log});
            }
        }
    }
}
