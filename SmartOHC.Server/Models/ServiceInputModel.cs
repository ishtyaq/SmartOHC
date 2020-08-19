using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Models
{
    
    public class ServiceInputModel
    {
        public ServiceType ServiceTypeId { get; set; } // 1: Health, 2 Blood, 3: temprature, 4: pulse

        public ActionType ActionTypeId { get; set; } // Start, Restart, Cancel

        public string CounterCode { get; set; }

        public ClientCode ClientCodeId { get; set; }

        public string CustomerCode { get; set; }

    }

    [Flags]
    public enum ServiceType
    {
        None = 0,
        Health = 1,
        Blood = 2,
        Temprature = 3,
        Pulse = 4
    }
    [Flags]
    public enum ActionType
    {
        None = 0,
        Start = 1,
        Restart = 2,
        Cancel = 3
    }
    [Flags]
    public enum ClientCode
    {
        None = 0,
        Tab = 1,
        Web = 2
    }
}
