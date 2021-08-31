using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Dtos;

namespace flowerlyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userSevice;
        public UserController(IUserService userSevice)
        {
            _userSevice = userSevice;
        }

        [HttpPut]
        public async Task UpsertUser(UserDto user)
        {
            await _userSevice.SaveUser(user);
        }
    }
}