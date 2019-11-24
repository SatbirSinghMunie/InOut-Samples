using APIResource.Infrastructure.Policies;
using APIResource.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;
        private IHttpRequestHelper _httpRequestHelper;

        public IdentityService(IHttpContextAccessor context, IConfiguration configuration, IHttpRequestHelper httpRequestHelper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _httpRequestHelper = httpRequestHelper;
        }

        public Int64 GetOrganizationIdFromHeader()
        {
            long organizationId = 0;
            Int64.TryParse(_context.HttpContext.Request.Headers[InOutConstants.OrganizationIdHeader], out organizationId);
            return organizationId;
        }

        public IEnumerable<KeyValueModel> GetUserClaims()
        {
            return _context.HttpContext.User.Claims.Select(x => new KeyValueModel()
            {
                Key = x.Type,
                Value = x.Value
            });
        }

        public async Task<List<ActivityViewModel>> GetUserActivities(string userEmail)
        {
            if (String.IsNullOrWhiteSpace(userEmail) || GetOrganizationIdFromHeader() <= 0)
            {
                return new List<ActivityViewModel>();
            }
            else
            {
                string applicationId = _configuration["SampleApplicationId"].ToString();
                string url = _configuration["AuthorizationServer"] + "api/v1/Authorization/auth-activities?userEmail=" + userEmail + "&applicationId=" + applicationId + "&organizationId=" + GetOrganizationIdFromHeader();
                var activityResponse = await _httpRequestHelper.GetRequestWithToken<List<ActivityViewModel>>(url);

                return activityResponse.Data;
            }
        }

        public async Task<User_ApiModel> AddUpdateUser(User_ApiModel user_ApiModel)
        {
            if (user_ApiModel == null)
            {
                return new User_ApiModel();
            }
            else
            {
                string url = _configuration["AuthorizationServer"] + "api/v1/Authorization/user";
                var userRolesResponse = await _httpRequestHelper.PostRequestWithToken<User_ApiModel, User_ApiModel>(url, user_ApiModel);

                return userRolesResponse.Data;
            }
        }

        public async Task<UserRoles_ApiModel> GetUserRoles()
        {
            string applicationId = _configuration["SampleApplicationId"].ToString();
            string emailId = _context.HttpContext.Request.Headers[InOutConstants.UserEmailHeader].ToString().ToLower();
            string url = _configuration["AuthorizationServer"] + "api/v1/Authorization/userroles?userEmail=" + emailId + "&applicationId=" + applicationId;
            var userRolesResponse = await _httpRequestHelper.GetRequestWithToken<UserRoles_ApiModel>(url);

            return userRolesResponse.Data;
        }

        public async Task<UserRoles_ApiModel> AddUpdateUserRoles(UserRoles_ApiModel userRoles_ApiModel)
        {
            if (userRoles_ApiModel == null)
            {
                return new UserRoles_ApiModel();
            }
            else
            {
                string url = _configuration["AuthorizationServer"] + "api/v1/Authorization/userroles";
                var userRolesResponse = await _httpRequestHelper.PostRequestWithToken<UserRoles_ApiModel, UserRoles_ApiModel>(url, userRoles_ApiModel);

                return userRolesResponse.Data;
            }
        }

        public async Task<bool> RemoveUserRoles(User_ApiModel user_ApiModel)
        {
            if (user_ApiModel == null)
            {
                return false;
            }
            else
            {
                string url = _configuration["AuthorizationServer"] + "api/v1/Authorization/userroles";
                var userRolesResponse = await _httpRequestHelper.PutRequestWithToken<bool, User_ApiModel>(url, user_ApiModel);

                return userRolesResponse.Data;
            }
        }
    }
}
