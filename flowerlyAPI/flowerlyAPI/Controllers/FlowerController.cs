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

        [HttpGet]
        public async Task<ActionResult<List<Flower>>> GetAll()
        {
            return Ok();
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddNewFlowerType(FlowerDto flowersDto)
        {
            return Ok();
        }
    }
}