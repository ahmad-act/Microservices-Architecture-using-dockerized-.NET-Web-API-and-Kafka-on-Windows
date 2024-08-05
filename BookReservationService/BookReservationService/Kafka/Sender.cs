using Confluent.Kafka;
using Newtonsoft.Json;

namespace BookReservationService.Kafka;

public static class Sender
{
    public static async Task<bool> Send<T>(this KafkaConfig kafkaConfig, T messageObject, ILogger<object> logger)
    {
        ProducerConfig producerConfig = new ProducerConfig
        {
            BootstrapServers = kafkaConfig.BootstrapServers,
            LingerMs = 1,
            BatchSize = 209715200, // 200 MB
            Acks = Acks.Leader
        };

        // Serialize the JSON object to a string
        string message = JsonConvert.SerializeObject(messageObject);

        using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
        {
            try
            {
                var result = await producer.ProduceAsync(kafkaConfig.Topic, new Message<Null, string>
                {
                    Value = message
                });

                Console.WriteLine($"Produced: {message}");

                producer.Flush(TimeSpan.FromSeconds(10));

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                logger?.LogError(ex.Message, ex);
            }
        }

        return false;
    }
}

