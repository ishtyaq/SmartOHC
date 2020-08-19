using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace SmartOHC.Server.Persistence
{
    public class LogDataModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string ServiceName { get; set; }
        public string ClientCode { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
