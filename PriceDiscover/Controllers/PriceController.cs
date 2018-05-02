using System.Collections.Generic;
using EventBus;
using Microsoft.AspNetCore.Mvc;

namespace PriceDiscover.Controllers
{
    [Route("api/[controller]")]
    public class PriceController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var integration = new IntegrationEvent();

            integration.Publish("Teste de mensagem");
            
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}