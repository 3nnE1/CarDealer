using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class SoldCarService : ISoldCarService
    {
        private readonly CarDealerContext _context;
        private readonly ILogger<SoldCarService> _logger;

        public SoldCarService(CarDealerContext context, ILogger<SoldCarService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<SoldCar> GetByID(Guid soldID)
        {
            try
            {
                SoldCar? sold = await _context.SoldCars.FindAsync(soldID);

                if (sold == null)
                {
                    _logger.LogWarning($"Sold: {soldID}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Sold: {soldID}, has been retrieved successfully.");
                }

                return sold;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sold ID: {soldID}.");
                return null;
            }
        }


        public async Task<SoldCar> GetByCar(Guid carID)
        {
            try
            {
                SoldCar? sold = await _context.SoldCars.FindAsync(carID);

                if (sold == null)
                {
                    _logger.LogWarning($"Sold Car ID: {carID}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Sold Car ID: {carID}, has been retrieved successfully.");
                }

                return sold;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sold Car ID: {carID}.");
                return null;
            }
        }


        public async Task<List<SoldCar>> GetBySalesManager(Guid salesManagerID)
        {
            try
            {
                string guid = salesManagerID.ToString();

                if (string.IsNullOrEmpty(guid))
                {
                    _logger.LogWarning($"Sales managed by Sales Manager: {salesManagerID}, not found.");
                    return new List<SoldCar>();
                }
                else
                {
                    _logger.LogInformation($"Sales managed by Sales Manager: {salesManagerID}, has been retrieved successfully.");

                    return await _context.SoldCars
                    .Where(a => a.Sales_Manager_ID == salesManagerID)
                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sales managed by Sales Manager: {salesManagerID}.");
                return null;
            }
        }

        public async Task<List<SoldCar>> GetByCustomer(Guid customerID)
        {
            try
            {
                string guid = customerID.ToString();

                if (string.IsNullOrEmpty(guid))
                {
                    _logger.LogWarning($"Purchase made by Customer: {customerID}, not found.");
                    return new List<SoldCar>();
                }
                else
                {
                    _logger.LogInformation($"Purchase made by Customer: {customerID}, has been retrieved successfully.");
                    
                    return await _context.SoldCars
                    .Where(a => a.Customer_ID == customerID)
                    .ToListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Purchase made by Customer: {customerID}.");
                return null;
            }
        }


        public async Task<SoldCar> Edit(Guid guid, SoldCarData soldCarData)
        {
            try
            {
                SoldCar? car = await _context.SoldCars.FindAsync(guid);

                if (car == null)
                {
                    _logger.LogWarning($"The Vehicle {guid} is not found");
                    return null;
                }
                else
                {
                    car.Car_ID = soldCarData.Car_ID;
                    car.Sales_Manager_ID = soldCarData.Sales_Manager_ID;
                    car.Customer_ID = soldCarData.Customer_ID;
                    car.SaleDate = soldCarData.SaleDate;
                    car.SalePrice = soldCarData.SalePrice;
                    car.Payment_Method = soldCarData.Payment_Method;
                    car.Warranty_Months = soldCarData.Warranty_Months;
                    car.Trade_In = soldCarData.Trade_In;
                    car.Sales_Manager_ID = soldCarData.Sales_Manager_ID;
                    car.Car_Image = soldCarData.Car_Image;

                    _context.SoldCars.Update(car);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"The Vehicle {guid} has been updated successfully.");
                    return car;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Vehicle {guid}.");
                return null;
            }
        }

        public async Task<bool> Remove(SoldCar car)
        {
            _logger.LogInformation($"{car.Sale_ID} is not more available.");
            _context.SoldCars.Remove(car);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}