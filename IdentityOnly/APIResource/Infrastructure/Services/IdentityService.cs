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
        
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<KeyValueModel> GetUserClaims()
        {
            return _context.HttpContext.User.Claims.Select(x => new KeyValueModel()
            {
                Key = x.Type,
                Value = x.Value
            });
        }
    }
}
