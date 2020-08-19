using SmartOHC.Server.Models;
using SmartOHC.Server.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOHC.Server.Services
{
    public class BodioService: IBodioService
    {
        private readonly MainDbContext _mainDbContext;
        public BodioService(MainDbContext mainDbContext)
        {
            this._mainDbContext = mainDbContext;
        }
        public async Task<bool> SaveSignalAsync(LogInputModel InputModel)
        {
            try
            {
                LogDataModel signalModel = new LogDataModel();
                signalModel.ServiceName = InputModel.ServiceName;
                signalModel.Description = InputModel.Description;
                signalModel.ClientCode = InputModel.ClientCode;
                signalModel.Status = InputModel.Status;
                signalModel.CreatedBy = InputModel.CreatedBy;
                signalModel.CreatedDate = DateTime.Now;

                _mainDbContext.Logs.Add(signalModel);
                return await _mainDbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
