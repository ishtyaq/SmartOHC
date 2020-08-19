using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SmartOHC.Server.Hubs;
using SmartOHC.Server.Models;
using SmartOHC.Server.Persistence;
using SmartOHC.Server.Services;

namespace SmartOHC.Server.Controllers
{


    [Route("api/bodio")]
    [ApiController]
    public class BodioController : ControllerBase
    {
        private readonly IBodioService _bodioService;
        private readonly IHubContext<SignalHub> _hubContext;

        public BodioController(IBodioService bodioService, IHubContext<SignalHub> hubContext)
        {
            _bodioService = bodioService;
            _hubContext = hubContext;
        }

        [HttpPost]
        [Route("healthcheck")]
        [ProducesResponseType(404)]
        [ProducesResponseType(202,Type= typeof(bool))]
        public async Task<ActionResult> SignalsArrived(ServiceInputModel InputModel)
        {
            LogInputModel signalViewModel = new LogInputModel
            {
                Description = "",
                ServiceName = Enum.GetName(typeof(ServiceType), InputModel.ServiceTypeId),
                Status = "Started..",
                ClientCode = Enum.GetName(typeof(ClientCode), InputModel.ClientCodeId),
                CreatedBy = InputModel.CustomerCode,
                CreatedDate = DateTime.Now
                //  SignalDate = Guid.NewGuid().ToString()
            };
            await _hubContext.Clients.All.SendAsync("SignalMessageReceived", signalViewModel);

            var serviceResponse = await CheckHealth(InputModel);
           // string serviceResponse = " {\"HealthResult\":{\"Height\":\"170.0 cm\",\"Weight\":\"84.6 kg\",\"BMI\":\"29.3\",\"RangeBMI\":\"19.5 - 25.5\",\"FatWeight\":\" kg\",\"FatBMR\":null,\"FatTBWR\":\"%\",\"FatSWT\":\" kg\",\"FatSFFM\":\" kg\",\"FatSMR\":\"%\",\"FatSDBZ\":\" kg\",\"FatSWJY\":\" kg\",\"FatSVFI\":\" kg\",\"FatSBMC\":\" kg\",\"FatSTBS\":null,\"FatSBDA\":null,\"RangeFat\":\"10.5 % - 21.5 %\",\"RangeFatWeight\":\"10.5 % - 21.5 %\",\"RangeFatBMR\":\"1395 - 1705 Kcal\",\"RangeFatTBWR\":\"50 % - 65 %\",\"RangeFatSWT\":\"42.3 kg - 55.0 kg\",\"RangeFatSMR\":\"31 % - 34 %\",\"BT\":null,\"RangeBT\":\"36°C - 37°C\",\"BO\":\"%\",\"RangeBO\":\"91%\",\"BPH\":\" mmHg\",\"RangeBPH\":\"90 mmHg - 139 mmHg\",\"BPL\":\" mmHg\",\"RangeBPL\":\"60 mmHg - 89 mmHg\",\"BPP\":null,\"RangeBPP\":null}\"}";
                  //  JsonConvert.SerializeObject()

          // HealthResult healthResult = JsonConvert.DeserializeObject<HealthResult>(serviceResponse);

             
            signalViewModel.Description = serviceResponse;
            signalViewModel.Status = "Success";
            signalViewModel.CreatedDate = DateTime.Now;
            var saveResult = await _bodioService.SaveSignalAsync(signalViewModel);
            if (saveResult)
            {

                await _hubContext.Clients.All.SendAsync("SignalMessageReceived", signalViewModel);

                //   _hubContext.Clients.
                // await _hubContext.Clients.All.SendAsync("Signal Message received", signalViewModel);
            }


            return StatusCode(200, saveResult);
        }

        public async Task<String> CheckHealth(ServiceInputModel InputModel)
        {
            string apiResponse = "";
            HealthResult healthResult;
            using (var httpClient = new HttpClient())
            {
                // StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
                String serviceURL = "http://10.150.50.136:8555/KioskService/api/HeightAndWeightMeasurement/33/1";
                if (InputModel.ServiceTypeId == ServiceType.Health)
                    serviceURL = "http://10.150.50.136:8555/KioskService/api/HeightAndWeightMeasurement/33/1";
                else if(InputModel.ServiceTypeId == ServiceType.Blood)
                    serviceURL = "http://10.150.50.136:8555/KioskService/api/BloodPresureMeasurement/33/1";
                else if (InputModel.ServiceTypeId == ServiceType.Temprature)
                    serviceURL = "http://10.150.50.136:8555/KioskService/api/TemperatureMeasurement/33/1";
                else if (InputModel.ServiceTypeId == ServiceType.Pulse)
                    serviceURL = "http://10.150.50.136:8555/KioskService/api/PulseMeasurement/33/1";

                //String serviceURL = "http://localhost:54849/weatherforecast";
                using (var response = await httpClient.GetAsync(serviceURL))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                   // string testResponse = "{\"Height\":\"170.0 cm\",\"Weight\":\"84.6 kg\",\"BMI\":\"29.3\",\"RangeBMI\":\"19.5 - 25.5\",\"FatWeight\":\" kg\",\"FatBMR\":null,\"FatTBWR\":\"%\",\"FatSWT\":\" kg\",\"FatSFFM\":\" kg\",\"FatSMR\":\"%\",\"FatSDBZ\":\" kg\",\"FatSWJY\":\" kg\",\"FatSVFI\":\" kg\",\"FatSBMC\":\" kg\",\"FatSTBS\":null,\"FatSBDA\":null,\"RangeFat\":\"10.5 % - 21.5 %\",\"RangeFatWeight\":\"10.5 % - 21.5 %\",\"RangeFatBMR\":\"1395 - 1705 Kcal\",\"RangeFatTBWR\":\"50 % - 65 %\",\"RangeFatSWT\":\"42.3 kg - 55.0 kg\",\"RangeFatSMR\":\"31 % - 34 %\",\"BT\":null,\"RangeBT\":\"36°C - 37°C\",\"BO\":\"%\",\"RangeBO\":\"91%\",\"BPH\":\" mmHg\",\"RangeBPH\":\"90 mmHg - 139 mmHg\",\"BPL\":\" mmHg\",\"RangeBPL\":\"60 mmHg - 89 mmHg\",\"BPP\":null,\"RangeBPP\":null}"
                  //  JsonConvert.SerializeObject()

                   //   healthResult =  healthResult = JsonConvert.DeserializeObject<HealthResult>(testResponse);
                }
            }
            //return healthResult.ToString();
             return apiResponse;
        }
    }
}
