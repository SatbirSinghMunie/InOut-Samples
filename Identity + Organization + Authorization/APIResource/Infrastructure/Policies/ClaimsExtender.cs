using APIResource.Infrastructure.Services;
using APIResource.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Policies
{
    public class ClaimsExtender : IClaimsTransformation
    {
        IIdentityService _identityService;

        public ClaimsExtender(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            string userEmail = principal.Identity.Name;

            List<ActivityViewModel> userActivities = await _identityService.GetUserActivities(userEmail);

            //Add claims if there are any allowed activities
            foreach (var activity in userActivities)
            {
                ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("Activity", activity.Name));
            }
            return await Task.FromResult(principal);
        }
    }
}
