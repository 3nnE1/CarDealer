using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.ConstrainedExecution;

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
        public async Task<AvailableCar> GetByID(Guid guid)
        {
            try
            {
                if (ExistsID(guid))                                                     
                {
                    AvailableCar? availableCar = await _context.AvailableCars.FindAsync(guid);
                    _logger.LogInformation($"Vehicle: {guid}, has been retrieved successfully.");
                    return availableCar;
                }
                else
                {
                    _logger.LogWarning($"Vehicle: {guid}, not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting car with ID: {guid}.");
                return null;
            }
        }
        #endregion


        #region Get By Plate
        public async Task<AvailableCar> GetByPlate(string plate)
        {
            try
            {
                if (ExistsPlate(plate))                         
                {
                    AvailableCar? car = await _context.AvailableCars
                    .FirstOrDefaultAsync(c => c.License_Plate == plate);

                    _logger.LogInformation($"Vehicle: {plate}, has been retrieved successfully.");
                    return car;
                }
                else
                {
                    _logger.LogWarning($"Vehicle: {plate}, not found.");
                    return null;
                }
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
            try
            {
                if (string.IsNullOrEmpty(motorType))
                {
                    _logger.LogWarning("The input string is null or empty");
                    return new List<AvailableCar>();
                }
                else
                {
                    List<AvailableCar> carsList = await _context.AvailableCars
                        .Where(a => a.Engine.ToLower().Contains(motorType.ToLower()))
                        .ToListAsync();
                    
                    if (carsList.Any())
                    {
                        _logger.LogInformation("Here is the Vehicles List");
                        return carsList;
                    }

                    _logger.LogWarning($"No car found with engine: {motorType}");
                    return new List<AvailableCar>();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occurred while finding the engine: {motorType}");
                return new List<AvailableCar>();
            }
        }
        #endregion


        #region Add
        public async Task<AvailableCar> AddAvailableCar(AvailableCarData carData)
        {
            try
            {
                var availableCar = new AvailableCar
                {
                    License_Plate = carData.License_Plate,
                    Segment = carData.Segment,
                    Model = carData.Model,
                    Year = carData.Year,
                    Engine = carData.Engine,
                    Transmission = carData.Transmission,
                    Mileage = carData.Mileage,
                    Fuel_Economy = carData.Fuel_Economy,
                    Key_Features = carData.Key_Features,
                    Paint_Color = carData.Paint_Color,
                    Interior_Color = carData.Interior_Color,
                    Accessories = carData.Accessories,
                    Cost = carData.Cost,
                    Arrival_Date = carData.Arrival_Date,
                    Car_Image = carData.Car_Image,
                    Intern_Image = carData.Intern_Image
                };

                _context.AvailableCars.Add(availableCar);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Vehicle added successfully: {availableCar}");
                return availableCar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a Vehicle: {carData}");
                return null;
            }
        }
        #endregion


        #region Edit By ID
        public async Task<AvailableCar> EditByID(Guid guid, AvailableCarData carData)
        {
            try
            {
                if (ExistsID(guid))
                {
                    AvailableCar? car = await _context.AvailableCars.FindAsync(guid);

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
                else
                {
                    _logger.LogWarning($"Can not update the Vehicle {guid}");
                    return null;
                }
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
                if (ExistsPlate(plate))
                {
                    AvailableCar? car = await _context.AvailableCars
                     .FirstOrDefaultAsync(c => c.License_Plate == plate);

                    car.License_Plate = plate;
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
                else
                {
                    _logger.LogWarning($"Can not update the Vehicle {plate}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Vehicle {plate}.");
                return null;
            }
        }
        #endregion


        #region Sell
        public async Task<SoldCar> Sell(Guid guid, SoldCarData soldCarData)
        {
            try
            {
                if (ExistsID(guid))
                {
                    AvailableCar? availableCar = await _context.AvailableCars.FindAsync(guid);
                    SoldCar soldCar = new SoldCar
                    {
                        Sale_ID = new Guid(),
                        Car_ID = guid,
                        License_Plate = availableCar.License_Plate,
                        Sales_Manager_ID = soldCarData.Sales_Manager_ID,
                        Customer_ID = soldCarData.Customer_ID,
                        Sale_Date = soldCarData.Sale_Date,
                        Sale_Price = soldCarData.Sale_Price,
                        Payment_Method = soldCarData.Payment_Method,
                        Warranty_Months = soldCarData.Warranty_Months,
                        Trade_In = soldCarData.Trade_In,
                        Sale_Manager_Image = null,
                        Car_Image = null,
                    };

                    await _context.SoldCars.AddAsync(soldCar);
                    _context.AvailableCars.Remove(availableCar);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"The Vehicle {guid} has been sold successfully.");
                    return soldCar;
                }
                else
                {
                    _logger.LogWarning($"Can not sell the Vehicle {guid}.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while selling the Vehicle {guid}.");
                return null;
            }
        }
        #endregion


        #region Delete By ID
        public async Task<string> DeleteByID(Guid guid)
        {
            try
            {
                if (ExistsID(guid))
                {
                    AvailableCar? car = await _context.AvailableCars.FindAsync(guid);
                    _context.AvailableCars.Remove(car);
                    _context.SaveChanges();
                    _logger.LogInformation($"The Vehicle {guid} has been deleted successfully.");

                    return $"The Vehicle {guid} has been deleted successfully.";
                }
                else
                {
                    _logger.LogWarning($"Can not delete the Vehicle {guid}");

                    return $"Can not delete the Vehicle {guid}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the Vehicle {guid}.");
                return $"Error occurred while deleting the Vehicle {guid}.";
            }
        }
        #endregion


        #region Delete By Plate
        public async Task<string> DeleteByPlate(string plate)
        {
            try
            {
                if (ExistsPlate(plate))
                {
                    AvailableCar? car = await _context.AvailableCars
                     .FirstOrDefaultAsync(c => c.License_Plate == plate);
                    
                    _context.AvailableCars.Remove(car);
                    _context.SaveChanges();      
                    _logger.LogInformation($"The Vehicle {plate} has been deleted successfully.");
              
                    return $"The Vehicle {plate} has been deleted successfully.";
                }
                else
                {
                    _logger.LogWarning($"Can not delete the Vehicle {plate}");
                    return $"Can not delete the Vehicle {plate}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the Vehicle {plate}.");
                return $"Error occurred while deleting the Vehicle {plate}.";
            }
        }
        #endregion


        #region Exists Guid
        public bool ExistsID(Guid guid)
        {
            if (_context.AvailableCars.Any(c => c.Car_ID == guid))
            {
                _logger.LogInformation("The Vehicle Exists!");
                return true;
            }
            else
            {
                _logger.LogInformation("The Vehicle Does Not Exists.");
                return false;
            }
        }
        #endregion


        #region Exists Plate
        public bool ExistsPlate(string plate)
        {
            if (_context.AvailableCars.Any(c => c.License_Plate == plate))
            {
                _logger.LogInformation("The Vehicle Exists.");
                return true;
            }
            else
            {
                _logger.LogInformation("The Vehicle Does Not Exists.");
                return false;
            }
        }
        #endregion


        
    }
}