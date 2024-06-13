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
        Task<bool> Add(AvailableCar car);
        Task<AvailableCar> EditByID(Guid guid, AvailableCarData carData);
        Task<AvailableCar> EditByPlate(string plate, AvailableCarData carData);

        Task<bool> Sell(AvailableCar car,
            Guid salesManager,
            Guid customer,
            int day,
            int month,
            int year,
            decimal salePrice,
            string paymentMethod,
            byte warrantyMonths,
            bool tradeIn);
      

        bool Exists(string plate);
    }
}
