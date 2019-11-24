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
    public enum ExternalAPIApplication
    {
        ORGM,
        Auth
    }
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        private IConfiguration _configuration;
        private const string OrganizationIdHeader = "OrganizationId";

        public IdentityService(
                        IHttpContextAccessor context,
                        IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
        }

        public Int64 GetOrganizationIdFromHeader()
        {
            long organizationId = 0;
            Int64.TryParse(_context.HttpContext.Request.Headers[OrganizationIdHeader], out organizationId);
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

        public List<ActivityViewModel> GetUserActivities(long organizationId, string userEmail)
        {
            //Custom Authorization implementation
            List<ActivityViewModel> activityList = new List<ActivityViewModel>();
            if (string.IsNullOrWhiteSpace(userEmail) || organizationId < 1)
            {
                return activityList;
            }

            if(userEmail.IndexOf("paxcom",StringComparison.InvariantCultureIgnoreCase) > -1)
            {
                activityList.Add(new ActivityViewModel() { Name = ActivityPolicies.VIEW_PAXCOM_DATA });
            }
            return activityList;
        }
    }
}
