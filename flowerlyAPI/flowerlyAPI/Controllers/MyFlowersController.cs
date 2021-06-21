using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace flowerlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyFlowersController : ControllerBase
    {

        [HttpGet]
        public async Task<List<MyFlowers>> GetMyFlowers(string id)
        {
            return new List<MyFlowers>();
        }

        [HttpGet("dates")]
        public async Task<List<IrrigationDates>> GetMyFlowersIrrigationDates(string userId)
        {
            return new List<IrrigationDates>();
        }

        [HttpGet("history")]
        public async Task<List<IrrigationDates>> GetMyFlowersIrrigationHistory(int flowerId)
        {
            return new List<IrrigationDates>();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewFlower(MyFlowersDto flowersDto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateFlower(MyFlowersUpdateCommand flowersDto)
        {
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteFlower(int myFlowersId)
        {
            return Ok();
        }


    }
}