using Confluent.Kafka;
using Newtonsoft.Json;

namespace BookInformationService.Kafka;

public static class Receiver
{
    public static List<string> Receive(this KafkaConfig kafkaConfig, ILogger<object> logger)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaConfig.BootstrapServers,
            GroupId = kafkaConfig.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe(kafkaConfig.Topic);

            List<string> messages = new List<string>();

            try
            {
                while (true)
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(15));

                    // Check if consumeResult is null (no message consumed within the timeout period)
                    if (consumeResult == null)
                    {
                        break;
                    }

                    messages.Add(consumeResult.Message.Value);

                    // Manually commit the offset of the consumed message
                    consumer.Commit(consumeResult);
                }

                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                consumer.Close();
            }
        }

        return null;
    }

}

