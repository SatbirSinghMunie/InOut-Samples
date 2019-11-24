using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIResource.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIResource.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IIdentityService _identityService;
        public UsersController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        // GET: api/Users
        [HttpGet]
        [Route("Claims")]
        public ActionResult GetUserClaims()
        {
            return new JsonResult(_identityService.GetUserClaims());
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
