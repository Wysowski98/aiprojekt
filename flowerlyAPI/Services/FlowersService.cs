using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database;
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services
{
    public class FlowersService : IFlowersService
    {
        private readonly FlowerlyDbContext _context;

        public FlowersService(FlowerlyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Flower>> GetAllFlowers()
        {
            return await _context.Flowers.ToListAsync();
        }

        public async Task AddFlower(FlowerDto flowerDto)
        {
            await _context.Flowers.AddAsync(new Flower { Name = flowerDto.Name, ImageUrl = flowerDto.ImageUrl, Nationality = flowerDto.Nationality, Species = flowerDto.Species });
            await _context.SaveChangesAsync();

        }
    }
}
