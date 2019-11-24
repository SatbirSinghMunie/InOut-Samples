using APIResource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Services
{
    public interface IIdentityService
    {
        IEnumerable<KeyValueModel> GetUserClaims();
    }
}
