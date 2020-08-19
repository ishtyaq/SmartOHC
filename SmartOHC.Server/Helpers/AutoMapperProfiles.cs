using AutoMapper;
using SmartOHC.Server.Models;
using SmartOHC.Server.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LogInputModel, LogDataModel>();
        }
    }
}
