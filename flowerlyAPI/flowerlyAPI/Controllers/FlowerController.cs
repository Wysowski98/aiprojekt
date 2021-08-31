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
    public class FlowerController : ControllerBase
    {
        private readonly IFlowersService _flowerService;

        public FlowerController(IFlowersService flowerSevice)
        {
            _flowerService = flowerSevice;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flower>>> GetAll()
        {
            return Ok(await _flowerService.GetAllFlowers());
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewFlowerType(FlowerDto flowersDto)
        {
            await _flowerService.AddFlower(flowersDto);
            return Ok();
        }
    }
}