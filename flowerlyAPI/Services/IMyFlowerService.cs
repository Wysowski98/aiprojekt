using Domain;
using Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IMyFlowerService
    {
        Task<List<MyFlowers>> GetMyFlowers(string id);
        Task AddFlower(MyFlowersDto flowersDto);
        Task DeleteFlower(int myFlowersId);
        Task UpdateFlower(MyFlowersUpdateCommand flowerDto);
        Task HandleConfirmation(string jobId, int historyId);
        Task<List<IrrigationDates>> GetMyIrrigationDates(string id);
        Task<List<IrrigationDates>> GetMyFlowersHistory(int flowerId);
    }
}