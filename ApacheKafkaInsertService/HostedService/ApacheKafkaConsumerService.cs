using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using HostedService.Data;

namespace HostedService
{
    public class ApacheKafkaConsumerService : IHostedService
    {
        private readonly string topic1 = "test";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        SyncConsumerContext _consumerContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ApacheKafkaConsumerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            IServiceScope scope = serviceScopeFactory.CreateScope();
            _consumerContext = scope.ServiceProvider.GetRequiredService<SyncConsumerContext>();
           
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
                            _consumerContext.SyncDataArchived.Add(orderRequest);
                            _consumerContext.SaveChanges();
                            Console.WriteLine($"Inserted the OrderRequest : {orderRequest.SyncId} for Update");
                        }        
                        
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
    

}

