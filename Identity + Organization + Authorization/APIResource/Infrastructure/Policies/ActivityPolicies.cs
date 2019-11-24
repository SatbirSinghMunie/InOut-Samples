using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Infrastructure.Policies
{
    public static class ActivityPolicies
    {
        public const string VIEW_PAXCOM_DATA = "View Paxcom Data";

        public static List<string> GetAllActivities()
        {
            List<string> activities = new List<string>();
            activities.Add(VIEW_PAXCOM_DATA);

            return activities;
        }

        public static IServiceCollection AddActivityAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                foreach (string activity in GetAllActivities())
                {
                    options.AddPolicy(activity, policy => policy.RequireClaim("Activity", activity));
                }
            });
            return services;
        }
    }
}
