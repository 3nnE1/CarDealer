using CarDealer.Controllers.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Entities;

namespace CarDealer.Services.Interfaces
{
    public interface ISoldCarService
    {
        Task<SoldCar> GetByID(Guid saleID);
        Task<SoldCar> GetByCar(Guid carID);
        Task<List<SoldCar>> GetBySalesManager(Guid salesManagerID);
        Task<List<SoldCar>> GetByCustomer(Guid customerID);
        Task<SoldCar> Edit(Guid guid, SoldCarData carData);
        Task<bool> Remove(SoldCar car);
    }
}
