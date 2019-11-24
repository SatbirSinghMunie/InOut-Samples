using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Models
{
    public class UserRoles_ApiModel
    {
        public long ApplicationId { get; set; }
        public long OrganizationId { get; set; }
        public string UserEmail { get; set; }
        public List<Role_ApiModel> Roles { get; set; }
    }
}
