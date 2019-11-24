using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIResource.Infrastructure.Policies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIResource.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Public Data 1", "Public Data 2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            return new string[] { "Private Data 1", "Private Data 2" };
        }

        [HttpGet("paxcom/{id}")]
        [Authorize(Policy = ActivityPolicies.VIEW_PAXCOM_DATA)]
        public ActionResult<IEnumerable<string>> GetPaxcomData(int id)
        {
            return new string[] { "Paxcom Data 1", "Paxcom Data 2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
