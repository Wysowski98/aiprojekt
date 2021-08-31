using Domain;
using Services.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IFlowersService
    {
        Task<List<Flower>> GetAllFlowers();
        Task AddFlower(FlowerDto flowerDto);
    }
}