using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Models
{
    public class LogInputModel
    {
        public string Description { get; set; }
        public string ServiceName { get; set; }
        public string ClientCode { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
