using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Domain;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Services.Dtos;

namespace Services
{
    public class MyFlowersService : IMyFlowerService
    {
        private readonly FlowerlyDbContext _context;
        private RecurringJobManager jobManager;

        public MyFlowersService(FlowerlyDbContext context)
        {
            _context = context;
            jobManager = new RecurringJobManager();
        }

        public async Task AddFlower(MyFlowersDto flowersDto)
        {
            var currentFlower = await _context.Flowers.FirstOrDefaultAsync(x => x.Id == flowersDto.FlowerId);
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UID == flowersDto.UserId);
            await _context.MyFlowers.AddAsync(new MyFlowers { Flower = currentFlower, User = currentUser });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFlower(MyFlowersUpdateCommand flowersDto)
        {
            var currentMyFlower = await _context.MyFlowers.FirstOrDefaultAsync(x => x.Id == flowersDto.FlowerId);

            var jobId = $"{Guid.NewGuid()}";
            flowersDto.Dates.ScheduledJobId = jobId;
            flowersDto.Dates.IsCompleted = false;

            if (currentMyFlower.IrrigationDates == null)
            {
                currentMyFlower.IrrigationDates = new List<IrrigationDates>();
                currentMyFlower.IrrigationDates.Add(flowersDto.Dates);
            }
            else
            {
                currentMyFlower.IrrigationDates.Add(flowersDto.Dates);
            }

            _context.MyFlowers.Update(currentMyFlower);
            await _context.SaveChangesAsync();

            jobManager.AddOrUpdate<IMailSenderService>(jobId, (IMailSenderService mc) => mc.SendMail(currentMyFlower.Id, jobId), GetCronDate(flowersDto.Dates.DayNumber));
        }

        private string GetCronDate(int dayNumber)
        {
            return $"0 0 * * {dayNumber}";
        }

        public async Task DeleteFlower(int myFlowersId)
        {
            var currentMyFlower = await _context.MyFlowers.FirstOrDefaultAsync(x => x.Id == myFlowersId);
            _context.MyFlowers.Remove(currentMyFlower);
            var dates = await _context.IrrigationDates.Where(x => x.MyFlowers.Id == myFlowersId).ToListAsync();
            _context.IrrigationDates.RemoveRange(dates);
            var history = await _context.IrrigationHistory.Where(x => x.MyFlower.Id == myFlowersId).ToListAsync();
            _context.IrrigationHistory.RemoveRange(history);
            foreach (var job in dates)
            {
                if (job.ScheduledJobId != null)
                {
                    jobManager.RemoveIfExists(job.ScheduledJobId);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<MyFlowers>> GetMyFlowers(string id)
        {
            var flowers = await _context.MyFlowers.Where(x => x.User.UID == id).Include(x => x.Flower).Include(y => y.IrrigationDates).ToListAsync();
            return flowers;
        }

        public async Task<List<IrrigationDates>> GetMyFlowersHistory(int flowerId)
        {
            var dates = await _context.IrrigationDates.Where(x => x.MyFlowers.Id == flowerId).Include(x => x.History).Include(y => y.MyFlowers).ToListAsync();
            return dates;
        }

        public async Task<List<IrrigationDates>> GetMyIrrigationDates(string id)
        {
            var dates = await _context.IrrigationDates.Where(x => x.MyFlowers.User.UID == id).Include(x => x.MyFlowers).ThenInclude(y => y.Flower).ToListAsync();
            return dates;
        }

        public async Task HandleConfirmation(string jobId, int historyId)
        {
            var currentJob = await _context.IrrigationDates.FirstOrDefaultAsync(x => x.ScheduledJobId == jobId);
            currentJob.IsCompleted = true;
            _context.IrrigationDates.Update(currentJob);
            var history = _context.IrrigationHistory.FirstOrDefault(x => x.Id == historyId);
            history.IsCompleted = true;
            history.IrrigationDate = DateTime.Now;
            _context.IrrigationHistory.Update(history);
            await _context.SaveChangesAsync();
        }
    }
}
