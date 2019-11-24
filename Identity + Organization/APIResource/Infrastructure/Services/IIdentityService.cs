using APIResource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Services
{
    public interface IIdentityService
    {
        Int64 GetOrganizationIdFromHeader();
        IEnumerable<KeyValueModel> GetUserClaims();
        List<ActivityViewModel> GetUserActivities(long organizationId, string userEmail);
    }
}
