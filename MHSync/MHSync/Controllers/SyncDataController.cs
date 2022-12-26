using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using MHSync.Data;
using MHSync.Model;

namespace MHSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncDataController : Controller
    {
        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "test";
        private readonly SyncDbContext _pcontext;

        public SyncDataController(SyncDbContext producercontext)
        {
            _pcontext = producercontext;
        }
        [HttpPost("/addSync")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<SyncData>> AddSync(SyncData sync)
        {
            try
            {
                if (sync == null)
                    return StatusCode(StatusCodes.Status400BadRequest, "Error creating new User Data record");
                SyncDataArchived syncdataarchive = new()
                {
                    SyncId = sync.SyncId,
                    Firstname = sync.Firstname,
                    Lastname = sync.Lastname,
                    Email = sync.Email,
                    Born = sync.Born,
                    Admission = sync.Admission,
                    PracticingArea = sync.PracticingArea,
                    PracticingLocation = sync.PracticingLocation,
                    Position = sync.Position,
                    PAN = null,
                    State = null,
                    Address = null,
                    ContactNumber = 0000000000
                };
                _pcontext.SyncDataArchive.Add(syncdataarchive);
                await _pcontext.SaveChangesAsync();
                var action = CreatedAtAction(nameof(PGetById), new { id = syncdataarchive.SyncId }, syncdataarchive);
                string message = JsonSerializer.Serialize(syncdataarchive);
                ProducerConfig config = new() { BootstrapServers = bootstrapServers, ClientId = Dns.GetHostName() };
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");

                }
                return action;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error While Adding Data {ex.Message}");
            }
        }


        [HttpGet("/get/{Id}")]
        public IActionResult PGetById(string Id)
        {
            var item = _pcontext.SyncDataArchive.FirstOrDefault(x => x.SyncId == Id); ;
            return item == null ? NotFound() : Ok(item);
        }

        [HttpGet("/getall")]
        public IEnumerable<SyncDataArchived> Get()
        {
            return _pcontext.SyncDataArchive;

        }


    }
}
