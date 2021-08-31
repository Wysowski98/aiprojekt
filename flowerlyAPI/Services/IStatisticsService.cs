using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IStatisticsService
    {
        Task<List<IrrigationHistory>> GetHistoryForUser(string id);
        Task<List<DateTime?>> GetNextExecutionTime(int myFlowerId);
    }
}
