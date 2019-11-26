using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            return new string[] { "Private Data 1", "Private Data 2" };
        }

        [HttpGet("paxcom/{id}")]
        [Authorize(Policy = "View Paxcom Data")]
        public ActionResult<IEnumerable<string>> GetPaxcomData(int id)
        {
            return new string[] { "Paxcom Data 1", "Paxcom Data 2" };
        }
    }
}
