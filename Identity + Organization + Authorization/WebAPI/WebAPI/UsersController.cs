using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaxcomAuth.Infrastructure.Services;
using PaxcomAuth.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IAuthorizationService _authorizationService;
        public UsersController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        //// GET: api/Users
        //[HttpGet]
        //[Route("Claims")]
        //public ActionResult GetUserClaims()
        //{
        //    return new JsonResult(_authorizationService.GetUserClaims());
        //}

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddUpdateUser(PaxcomAuth_User_ApiModel user)
        {
            //------------------------------------Add custom logic to store user info in local system------------------------------------//
           
            //Custom Add User logic

            //-------------------------Also add user info in Authorization server. Used for managing user roles-------------------------//
            var userModel = await _authorizationService.AddUpdateUser(user);
            return Ok(userModel);
        }

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult> GetUserRoles()
        {
            var userRoles = await _authorizationService.GetUserRoles();
            return Ok(userRoles);
        }

        [HttpPost]
        [Route("roles")]
        public async Task<ActionResult> AddRoles(PaxcomAuth_UserRoles_ApiModel model)
        {
            var userRoles = await _authorizationService.AddUpdateUserRoles(model);
            return Ok(userRoles);
        }

        [HttpPut]
        [Route("roles")]
        public async Task<ActionResult> UpdateRoles(PaxcomAuth_User_ApiModel user_ApiModel)
        {
            var success = await _authorizationService.RemoveUserRoles(user_ApiModel);
            return Ok(success);
        }

        [HttpGet]
        [Route("auth-activities")]
        public async Task<IActionResult> GetAuthorizationInfo(string userEmail)
        {
            List<PaxcomAuth_ActivityViewModel> userActivities = await _authorizationService.GetUserActivities(userEmail);
            return Ok(userActivities);
        }
    }
}
