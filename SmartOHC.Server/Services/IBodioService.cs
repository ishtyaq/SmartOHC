using SmartOHC.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Services
{
    public interface IBodioService
    {
        Task<bool> SaveSignalAsync(LogInputModel InputModel);
    }
}
