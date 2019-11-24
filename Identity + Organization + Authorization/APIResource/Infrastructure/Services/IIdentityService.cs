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
        Task<List<ActivityViewModel>> GetUserActivities(string userEmail);
        Task<User_ApiModel> AddUpdateUser(User_ApiModel user_ApiModel);
        Task<UserRoles_ApiModel> GetUserRoles();
        Task<UserRoles_ApiModel> AddUpdateUserRoles(UserRoles_ApiModel user_ApiModel);
        Task<bool> RemoveUserRoles(User_ApiModel user_ApiModel);
    }
}
