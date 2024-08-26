using Confluent.Kafka;
using System.Text.Json;

namespace pmt_app2
{
    public class HeartbeatService : BackgroundService
    {
        private readonly IKafkaProducerService _producerService;
        private readonly ILogger<HeartbeatService> _logger;

        public HeartbeatService(IKafkaProducerService producerService, ILogger<HeartbeatService> logger)
        {
            _producerService = producerService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Sending heartbeat from App 2");
                await _producerService.ProduceAsync("Hello there from App 2");
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Send heartbeat every 30 seconds
            }
        }
    }
}
