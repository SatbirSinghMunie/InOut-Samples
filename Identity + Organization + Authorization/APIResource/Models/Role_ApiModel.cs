using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Models
{
    public class Role_ApiModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public bool IsRoleSelected { get; set; }
        public RoleStatus Status { get; set; }
        public long ApplicationId { get; set; }
        public long OrganizationId { get; set; }
    }
    public enum RoleStatus : byte
    {
        Active = 1,
        Deactivated,
    }
}
