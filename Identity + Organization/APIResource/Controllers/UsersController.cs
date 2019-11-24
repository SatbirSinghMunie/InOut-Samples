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
        public ActionResult AddUpdateUser(User_ApiModel user)
        {
            return Ok(new User_ApiModel());
        }

        [HttpGet]
        [Route("roles")]
        public ActionResult GetUserRoles(string userEmail)
        {
            List<Role_ApiModel> userRoles = new List<Role_ApiModel>();
            userRoles.Add(new Role_ApiModel()
            {
                Id = 1,
                IsRoleSelected = true,
                Name = "Admin",
                Status = RoleStatus.Active
            });
            userRoles.Add(new Role_ApiModel()
            {
                Id = 2,
                IsRoleSelected = false,
                Name = "User",
                Status = RoleStatus.Active
            });
            userRoles.Add(new Role_ApiModel()
            {
                Id = 3,
                IsRoleSelected = false,
                Name = "SampleRole",
                Status = RoleStatus.Deactivated
            });

            BaseResponse<UserRoles_ApiModel> response = new BaseResponse<UserRoles_ApiModel>()
            {
                Data = new UserRoles_ApiModel()
                {
                    ApplicationId = 1000,
                    OrganizationId = 0,//_identityService.GetOrganizationIdFromHeader(),
                    Roles = userRoles,
                    UserEmail = userEmail
                }
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("roles")]
        public ActionResult AddRoles(Role_ApiModel model)
        {
            return Ok();
        }

        [HttpPut]
        [Route("roles")]
        public ActionResult UpdateRoles(Role_ApiModel model)
        {
            return Ok();
        }
    }
}
