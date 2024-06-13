using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class AvailableCarService : IAvailableCarService
    {
        #region Setup
        private readonly CarDealerContext _context;
        private readonly ILogger<AvailableCarService> _logger;

        public AvailableCarService(CarDealerContext context, ILogger<AvailableCarService> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion


        #region Get By ID
        public async Task<AvailableCar> GetByID(Guid carID)
        {
            try
            {
                AvailableCar? car = await _context.AvailableCars.FindAsync(carID);

                if (car == null)
                {
                    _logger.LogWarning($"Vehicle: {carID}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Vehicle: {carID}, has been retrieved successfully.");
                }

                return car;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting car with ID: {carID}.");
                return null;
            }
        }
        #endregion


        #region Get By Plate
        public async Task<AvailableCar> GetByPlate(string plate)
        {
            try
            {
                AvailableCar? car = await _context.AvailableCars
                    .FirstOrDefaultAsync(c => c.License_Plate == plate);

                if (car == null)
                {
                    _logger.LogWarning($"Vehicle: {plate}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Vehicle: {plate}, has been retrieved successfully.");
                }

                return car;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting car with plate: {plate}.");
                return null;
            }
        }
        #endregion


        #region Get By Engine
        public async Task<List<AvailableCar>> GetByEngine(string motorType)
        {
            if (string.IsNullOrEmpty(motorType))
            {
                return new List<AvailableCar>();
            }

            return await _context.AvailableCars
                .Where(a => a.Engine.ToLower().Contains(motorType.ToLower()))
                .ToListAsync();
        }
        #endregion


        #region Add
        public async Task<bool> Add(AvailableCar car)
        {
            try
            {
                _context.AvailableCars.Add(car);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Vehicle added successfully: {car}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a Vehicle: {car}");
                return false;
            }
        }
        #endregion


        #region Edit By ID
        public async Task<AvailableCar> EditByID(Guid guid, AvailableCarData carData)
        {
            try
            {
                AvailableCar? car = await _context.AvailableCars.FindAsync(guid);

                if (car == null)
                {
                    _logger.LogWarning($"The Vehicle {guid} is not found");
                    return null;
                }
                car.License_Plate = carData.License_Plate;
                car.Segment = carData.Segment;
                car.Model = carData.Model;
                car.Year = carData.Year;
                car.Engine = carData.Engine;
                car.Transmission = carData.Transmission;
                car.Mileage = carData.Mileage;
                car.Fuel_Economy = carData.Fuel_Economy;
                car.Key_Features = carData.Key_Features;
                car.Paint_Color = carData.Paint_Color;
                car.Interior_Color = carData.Interior_Color;
                car.Accessories = carData.Accessories;
                car.Cost = carData.Cost;
                car.Arrival_Date = carData.Arrival_Date;
                car.Car_Image = carData.Car_Image;
                car.Intern_Image = carData.Intern_Image;

                _context.AvailableCars.Update(car);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"The Vehicle {guid} has been updated successfully.");
                return car;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Vehicle {guid}.");
                return null;
            }
        }
        #endregion


        #region Edit By Plate
        public async Task<AvailableCar> EditByPlate(string plate, AvailableCarData carData)
        {
            try
            {
                AvailableCar? car = await _context.AvailableCars
                     .FirstOrDefaultAsync(c => c.License_Plate == plate);

                if (car == null)
                {
                    _logger.LogWarning($"The Vehicle {plate} is not found");
                    return null;
                }

                car.Segment = carData.Segment;
                car.Model = carData.Model;
                car.Year = carData.Year;
                car.Engine = carData.Engine;
                car.Transmission = carData.Transmission;
                car.Mileage = carData.Mileage;
                car.Fuel_Economy = carData.Fuel_Economy;
                car.Key_Features = carData.Key_Features;
                car.Paint_Color = carData.Paint_Color;
                car.Interior_Color = carData.Interior_Color;
                car.Accessories = carData.Accessories;
                car.Cost = carData.Cost;
                car.Arrival_Date = carData.Arrival_Date;
                car.Car_Image = carData.Car_Image;
                car.Intern_Image = carData.Intern_Image;

                _context.AvailableCars.Update(car);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"The Vehicle {plate} has been updated successfully.");
                return car;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Vehicle {plate}.");
                return null;
            }
        }
        #endregion

        #region Sell
        public async Task<bool> Sell(AvailableCar car, Guid salesManager, Guid customer, int day, int month, int year, decimal salePrice, string paymentMethod, byte warrantyMonths, bool tradeIn)
        {
            try
            {
                var soldCar = new SoldCar();

                soldCar.Sale_ID = new Guid();
                soldCar.Car_ID = car.Car_ID;
                soldCar.Sales_Manager_ID = salesManager;
                soldCar.Customer_ID = customer;
                soldCar.SaleDate = new DateTime(year, month, day);
                soldCar.SalePrice = salePrice;
                soldCar.Payment_Method = paymentMethod;
                soldCar.Warranty_Months = warrantyMonths;
                soldCar.Trade_In = tradeIn;
                soldCar.Sale_Manager_Image = null;
                soldCar.Car_Image = null;

                await _context.SoldCars.AddAsync(soldCar);
                _logger.LogInformation($"{soldCar.Car_ID} has been sold successfully.");

                _logger.LogInformation($"{car.Car_ID} is not more available.");
                _context.AvailableCars.Remove(car);

                await _context.SaveChangesAsync();
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while selling the Vehicle {car.Car_ID}");
                return false;
            }
        }
        #endregion


        #region Exists
        public bool Exists(string plate)
        {
            if (!_context.AvailableCars.Any(c => c.License_Plate == plate))
            {
                _logger.LogInformation("The Vehicle does not Exists.");
                return false;
            }
            else
            {
                _logger.LogWarning("The Vehicle already Exists!");
                return true;
            }
        }
        #endregion
    }
}
