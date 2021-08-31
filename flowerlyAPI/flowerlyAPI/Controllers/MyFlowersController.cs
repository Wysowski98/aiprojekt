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
        private readonly IMyFlowerService _flowerService;
        public MyFlowersController(IMyFlowerService flowerSevice)
        {
            _flowerService = flowerSevice;
        }

        [HttpGet]
        public async Task<List<MyFlowers>> GetMyFlowers(string id)
        {
            return await _flowerService.GetMyFlowers(id);
        }

        [HttpGet("dates")]
        public async Task<List<IrrigationDates>> GetMyFlowersIrrigationDates(string userId)
        {
            return await _flowerService.GetMyIrrigationDates(userId);
        }

        [HttpGet("history")]
        public async Task<List<IrrigationDates>> GetMyFlowersIrrigationHistory(int flowerId)
        {
            return await _flowerService.GetMyFlowersHistory(flowerId);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewFlower(MyFlowersDto flowersDto)
        {
            await _flowerService.AddFlower(flowersDto);
            return Ok();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateFlower(MyFlowersUpdateCommand flowersDto)
        {
            await _flowerService.UpdateFlower(flowersDto);
            return Ok();
        }

        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteFlower(int myFlowersId)
        {
            await _flowerService.DeleteFlower(myFlowersId);
            return Ok();
        }

        [HttpGet]
        [Route("emailConfirmation/{code}/{history}")]
        public async Task<IActionResult> AddNewFlower(string code, int history)
        {
            await _flowerService.HandleConfirmation(code, history);
            return Ok("Irregation Approved");
        }
    }
}