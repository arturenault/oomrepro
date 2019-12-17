using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OOMRepro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemoryController : ControllerBase
    {
        private readonly ILogger<MemoryController> _logger;

        private readonly Random random;

        public MemoryController(ILogger<MemoryController> logger)
        {
            _logger = logger;
            random = new Random();
        }

        [HttpGet]
        public MemoryMetrics Get()
        {
            var client = new MemoryMetricsClient();
            return client.GetMetrics();
        }

        [HttpPost("{size:int}")]
        public void Post(int size=1000000)
        {
            byte[] data = new byte[8192];
            var path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (FileStream stream = System.IO.File.OpenWrite(path))
            {
                for (int i = 0; i < size; i++)
                {
                    random.NextBytes(data);
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}
