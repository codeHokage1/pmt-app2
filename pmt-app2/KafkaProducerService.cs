using Confluent.Kafka;
using System.Text.Json;

namespace pmt_app2
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string message);
    }

    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            _topic = configuration["Kafka:ProducerTopic"];
        }

        public async Task ProduceAsync(string message)
        {
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }

}
