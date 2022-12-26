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
    public class ApacheKafkaUpdateService : IHostedService
    {
        
        private readonly string topic2 = "test1";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        SyncConsumerContext _consumerContext;
        SyncDbcontext _syncDbcontext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ApacheKafkaUpdateService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            IServiceScope scope = serviceScopeFactory.CreateScope();
            _consumerContext = scope.ServiceProvider.GetRequiredService<SyncConsumerContext>();
            _syncDbcontext = scope.ServiceProvider.GetRequiredService<SyncDbcontext>();
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

                    consumerBuilder.Subscribe(topic2);
                    var cancelToken = new CancellationTokenSource();

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume(cancelToken.Token);

                            var orderRequest = JsonSerializer.Deserialize<SyncDataArchived>(consumer.Message.Value);
                            _syncDbcontext.SyncDataArchive.Update(orderRequest);
                            _syncDbcontext.SaveChanges();
                            Console.WriteLine($"Updated the OrderRequest : {orderRequest.SyncId}");
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
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }


}

