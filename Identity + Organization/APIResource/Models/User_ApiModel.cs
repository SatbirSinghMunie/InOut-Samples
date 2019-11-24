using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Models
{
    public class User_ApiModel
    {
        public string Email { get; set; }
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public IList<Role_ApiModel> Roles { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public string ProfileImageUri { get; set; }
        public byte StatusId { get; set; }
        public string Name { get; set; }
    }
    public enum UserStatus : byte
    {
        New = 1,
        Active,
        Deactivated
    }
}
