using Database;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly FlowerlyDbContext _context;

        public UserService(FlowerlyDbContext context)
        {
            _context = context;
        }
        public async Task SaveUser(UserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UID == userDto.UID);

            if (user == null)
            {
                _context.Users.Add(new User { UID = userDto.UID, Email = userDto.Email, DisplayName = userDto.DisplayName, Flowers = new List<MyFlowers>() });
            }

            await _context.SaveChangesAsync();
        }
    }
}
