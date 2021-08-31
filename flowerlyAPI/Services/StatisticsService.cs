using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Domain;
using Hangfire;
using Hangfire.Storage;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class StatisticsService : IStatisticsService
    {
        FlowerlyDbContext _context;
        public StatisticsService(FlowerlyDbContext context)
        {
            _context = context;
        }

        public async Task<List<IrrigationHistory>> GetHistoryForUser(string id)
        {
            return await _context.IrrigationHistory.Where(x => x.User.UID == id).Include(x => x.User).Include(x => x.MyFlower).ToListAsync();
        }

        public async Task<List<DateTime?>> GetNextExecutionTime(int myFlowerId)
        {
            var dates = await _context.IrrigationDates.Where(x => x.MyFlowers.Id == myFlowerId).ToListAsync();
            List<DateTime?> result = new List<DateTime?>();

            foreach (var date in dates)
            {
                var recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
                var nextEx = recurringJobs.FirstOrDefault(x => x.Id == date.ScheduledJobId);
                if (nextEx != null)
                {
                    result.Add(nextEx.NextExecution);
                }
            }

            return result;
        }
    }
}
