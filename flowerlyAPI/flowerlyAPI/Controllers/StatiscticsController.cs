using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace flowerlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatiscticsController : ControllerBase
    {
        IStatisticsService statisticsService;
        public StatiscticsController(IStatisticsService _statisticsService)
        {
            statisticsService = _statisticsService;
        }

        [HttpGet("chart")]
        public async Task<List<IrrigationHistory>> GetMyIrrigationHistory(string id)
        {
            return await statisticsService.GetHistoryForUser(id);
        }

        [HttpGet("progreess")]
        public async Task<List<DateTime?>> GetDataForProgress(int myFlowerId)
        {
            return await statisticsService.GetNextExecutionTime(myFlowerId);
        }
    }
}