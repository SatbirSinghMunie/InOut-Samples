using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIResource.Infrastructure.Services;
using APIResource.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIResource.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> AddUpdateUser(User_ApiModel user)
        {
            //------------------------------------Add custom logic to store user info in local system------------------------------------//
                //Custom Add User logic
            //-------------------------Also add user info in Authorization server. Used for managing user roles-------------------------//
            User_ApiModel userModel = await _identityService.AddUpdateUser(user);

            BaseResponse<User_ApiModel> response = new BaseResponse<User_ApiModel>()
            {
                Data = userModel
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult> GetUserRoles()
        {
            UserRoles_ApiModel userRoles = await _identityService.GetUserRoles();

            BaseResponse<UserRoles_ApiModel> response = new BaseResponse<UserRoles_ApiModel>()
            {
                Data = userRoles
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("roles")]
        public async Task<ActionResult> AddRoles(UserRoles_ApiModel model)
        {
            UserRoles_ApiModel userRoles = await _identityService.AddUpdateUserRoles(model);

            BaseResponse<UserRoles_ApiModel> response = new BaseResponse<UserRoles_ApiModel>()
            {
                Data = userRoles
            };
            return Ok(response);
        }

        /// <summary>
        /// Used for deleting UserRoles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("roles")]
        public async Task<ActionResult> UpdateRoles(User_ApiModel user_ApiModel)
        {
            bool success = await _identityService.RemoveUserRoles(user_ApiModel);

            BaseResponse<bool> response = new BaseResponse<bool>()
            {
                Data = success
            };
            return Ok(response);
        }

        [HttpGet]
        [Route("auth-activities")]
        public async Task<IActionResult> GetAuthorizationInfo(string userEmail)
        {
            List<ActivityViewModel> userActivities = await _identityService.GetUserActivities(userEmail);
            return Ok(userActivities);
        }
    }
}
