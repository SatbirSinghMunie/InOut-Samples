using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIResource.Models
{
    public class ActivityViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ApplicationId { get; set; }
        public string ApplicationName { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
    }
}
