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
        Task<SoldCar> GetBySaleID(Guid saleID);
        Task<SoldCar> GetByCarID(Guid carID);
        Task<SoldCar> GetByCarPlate(string plate);
        Task<List<SoldCar>> GetBySalesManagerID(Guid salesManagerID);
        Task<List<SoldCar>> GetByCustomerID(Guid customerID);
        Task<SoldCar> AddSoldCar(SoldCarData soldCarData);
        Task<SoldCar> EditBySaleID(Guid guid, SoldCarData carData);
        Task<string> DeleteBySaleID(Guid guid);
        Task<string> DeleteByPlate(string plate);


        bool ExistsSaleID(Guid guid);
        bool ExistsCarID(Guid guid);
        bool ExistsPlate(string plate);
        bool ExistsSalesManagerID(Guid guid);
        bool ExistsCustomerID(Guid guid);
    }
}
