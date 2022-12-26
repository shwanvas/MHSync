using Confluent.Kafka;
using MHUpdateSync.Data;
using MHUpdateSync.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace MHUpdateSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncUpdateController : Controller
    {
        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "test1";
        private readonly SyncConsumerContext _pcontext;

        public SyncUpdateController(SyncConsumerContext producercontext)
        {
            _pcontext = producercontext;
        }
        [HttpPut("/updateSync")]
        public async Task<IActionResult> PutSyncItem(SyncDataArchived syncdataarchive) {
            _pcontext.Entry(syncdataarchive).State = EntityState.Modified;
            await _pcontext.SaveChangesAsync();
            
            string message = JsonSerializer.Serialize(syncdataarchive);
            ProducerConfig config = new() { BootstrapServers = bootstrapServers, ClientId = Dns.GetHostName() };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
            }
            return NoContent();

        }
	[HttpGet("/getall")]
        public IEnumerable<SyncDataArchived> Get()
        {
            return _pcontext.SyncDataArchived;

        }
    }
}
