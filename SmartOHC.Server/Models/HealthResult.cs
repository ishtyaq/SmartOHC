using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Models
{
    public class HealthResult
    {
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string RangeBMI { get; set; }
        public string FatWeight { get; set; }
        public object FatBMR { get; set; }
        public string FatTBWR { get; set; }
        public string FatSWT { get; set; }
        public string FatSFFM { get; set; }
        public string FatSMR { get; set; }
        public string FatSDBZ { get; set; }
        public string FatSWJY { get; set; }
        public string FatSVFI { get; set; }
        public string FatSBMC { get; set; }
        public object FatSTBS { get; set; }
        public object FatSBDA { get; set; }
        public string RangeFat { get; set; }
        public string RangeFatWeight { get; set; }
        public string RangeFatBMR { get; set; }
        public string RangeFatTBWR { get; set; }
        public string RangeFatSWT { get; set; }
        public string RangeFatSMR { get; set; }
        public object BT { get; set; }
        public string RangeBT { get; set; }
        public string BO { get; set; }
        public string RangeBO { get; set; }
        public string BPH { get; set; }
        public string RangeBPH { get; set; }
        public string BPL { get; set; }
        public string RangeBPL { get; set; }
        public object BPP { get; set; }
        public object RangeBPP { get; set; }
    }

}
