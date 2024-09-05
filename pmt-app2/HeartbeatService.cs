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
                var messageToSend = new AppInfo
                {
                    ApplicationName = "App2",
                    ApplicationId = "app2-XY",
                    Timestamp = DateTime.UtcNow
                };
                // await _producerService.ProduceAsync("How far!Let's do this monitoring app!");
                var heartbeatMessage = JsonSerializer.Serialize(messageToSend);
                await _producerService.ProduceAsync(heartbeatMessage);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Send heartbeat every 30 seconds
            }
        }
    }

    public class AppInfo
    {
        public string ApplicationName { get; set; }
        public string ApplicationId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
