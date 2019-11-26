using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIResource.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIResource.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/Users
        [HttpGet]
        [Route("Claims")]
        public ActionResult GetUserClaims()
        {
            IEnumerable<KeyValueModel> userClaims = HttpContext.User.Claims.Where(x =>x.Type.IndexOf("name") > -1).Select(x => new KeyValueModel()
            {
                Key = x.Type,
                Value = x.Value
            });
            return new JsonResult(userClaims);
        }
    }
}
