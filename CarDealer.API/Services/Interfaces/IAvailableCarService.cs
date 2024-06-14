using CarDealer.Controllers.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Entities;

namespace CarDealer.Services.Interfaces
{
    public interface IAvailableCarService
    {
        Task<AvailableCar> GetByID(Guid carID);
        Task<AvailableCar> GetByPlate(string plate);
        Task<List<AvailableCar>> GetByEngine(string motorType);
        Task<AvailableCar> AddAvailableCar(AvailableCarData car);
        Task<AvailableCar> EditByID(Guid guid, AvailableCarData carData);
        Task<AvailableCar> EditByPlate(string plate, AvailableCarData carData);
        Task<SoldCar> Sell(Guid guid, SoldCarData soldCarData);
        Task<string> DeleteByID(Guid guid);
        Task<string> DeleteByPlate(string plate);


        bool ExistsID(Guid guid);
        bool ExistsPlate(string plate);
    }
}
