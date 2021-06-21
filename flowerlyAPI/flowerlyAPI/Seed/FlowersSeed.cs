using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flowerlyAPI.Seed
{
    public class FlowersSeed: IFlowersSeed
    {
        private readonly FlowerlyDbContext _context;

        public FlowersSeed(FlowerlyDbContext context)
        {
            _context = context;
        }

        public async Task Handle()
        {
            if (! _context.Flowers.Any())
            {
                var sql = System.IO.File.ReadAllText("Seed\\RawSql\\SqlFlowerInsert.sql");
                await _context.Database.ExecuteSqlCommandAsync(sql);
            }
            
        }
    }
}
