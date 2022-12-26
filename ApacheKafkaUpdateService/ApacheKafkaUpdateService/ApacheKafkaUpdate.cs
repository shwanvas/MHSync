using ApacheKafkaUpdateService.Data;
using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Text.Json;

namespace ApacheKafkaUpdateService
{
    public class ApacheKafkaUpdate : IHostedService
    {
        private readonly string topic1 = "test1";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        SyncDbContext _consumerContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ApacheKafkaUpdate(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            IServiceScope scope = serviceScopeFactory.CreateScope();
            _consumerContext = scope.ServiceProvider.GetRequiredService<SyncDbContext>();

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {

                    consumerBuilder.Subscribe(topic1);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                                (cancelToken.Token);

                            var orderRequest = JsonSerializer.Deserialize
                                <SyncDataArchived>
                                    (consumer.Message.Value);
                            _consumerContext.Entry(orderRequest).State = EntityState.Modified;
                            _consumerContext.SaveChanges();
                            Console.WriteLine($"Updated the OrderRequest : {orderRequest.SyncId}");
                        }

                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
