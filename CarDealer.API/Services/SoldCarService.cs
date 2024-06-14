using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class SoldCarService : ISoldCarService
    {
        #region Setup
        private readonly CarDealerContext _context;
        private readonly ILogger<SoldCarService> _logger;

        public SoldCarService(CarDealerContext context, ILogger<SoldCarService> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion


        #region Get By Sale ID
        public async Task<SoldCar> GetBySaleID(Guid guid)
        {
            try
            {
                if (ExistsSaleID(guid))
                {
                    SoldCar? soldCar = await _context.SoldCars.FindAsync(guid);
                    _logger.LogInformation($"Sale: {guid}, has been retrieved successfully.");
                    return soldCar;
                }
                else
                {
                    _logger.LogWarning($"Sale: {guid}, not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting sale: {guid}.");
                return null;
            }
        }
        #endregion


        #region Get By Car ID
        public async Task<SoldCar> GetByCarID(Guid guid)
        {
            try
            {
                if (ExistsCarID(guid))
                {
                    SoldCar? car = await _context.SoldCars
                    .FirstOrDefaultAsync(c => c.Car_ID == guid);

                    _logger.LogInformation($"Vehicle: {guid}, has been retrieved successfully.");
                    return car;
                }
                else
                {
                    _logger.LogWarning($"Vehicle: {guid}, not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting car: {guid}.");
                return null;
            }
        }
        #endregion


        #region Get By Plate
        public async Task<SoldCar> GetByCarPlate(string plate)
        {
            try
            {
                if (ExistsPlate(plate))
                {
                    SoldCar? car = await _context.SoldCars
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


        #region Get By Sales Manager
        public async Task<List<SoldCar>> GetBySalesManagerID(Guid salesManagerId)
        {
            try
            {
                if (ExistsSalesManagerID(salesManagerId))
                {
                    List<SoldCar> carsList = await _context.SoldCars
                        .Where(a => a.Sales_Manager_ID == salesManagerId)
                        .ToListAsync();

                    _logger.LogInformation("Here is the Sales Manager sales List");
                    return carsList;
                }
                else
                {
                    _logger.LogWarning($"No sales found from the Sales Manager: {salesManagerId}");
                    return new List<SoldCar>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sales Manager: {salesManagerId} sales.");
                return null;
            }
        }
        #endregion


        #region Get By Customer
        public async Task<List<SoldCar>> GetByCustomerID(Guid customerID)
        {
            try
            {
                if (ExistsCustomerID(customerID))
                {
                    List<SoldCar> carsList = await _context.SoldCars
                        .Where(a => a.Customer_ID == customerID)
                        .ToListAsync();

                    _logger.LogInformation("Here is the Customer shop List");
                    return carsList;
                }
                else
                {
                    _logger.LogWarning($"No shops found from the Customer: {customerID}");
                    return new List<SoldCar>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Customer: {customerID} shops.");
                return null;
            }
        }
        #endregion


        #region Add Sold Car
        public async Task<SoldCar> AddSoldCar(SoldCarData carData)
        {
            try
            {
                SoldCar soldCar = new SoldCar
                {
                    Car_ID = carData.Car_ID,
                    License_Plate = carData.License_Plate,
                    Sales_Manager_ID = carData.Sales_Manager_ID,
                    Customer_ID = carData.Customer_ID,
                    Sale_Date = carData.Sale_Date,
                    Sale_Price = carData.Sale_Price,
                    Payment_Method = carData.Payment_Method,
                    Warranty_Months = carData.Warranty_Months,
                    Trade_In = carData.Trade_In,
                    Car_Image = carData.Car_Image,
                    Sale_Manager_Image = carData.Sale_Manager_Image
                };

                _context.SoldCars.Add(soldCar);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Sale added successfully: {soldCar}");
                return soldCar;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a Sale: {carData}");
                return null;
            }
        }
        #endregion


        #region Edit By Sale ID
        public async Task<SoldCar> EditBySaleID(Guid guid, SoldCarData carData)
        {
            try
            {
                if (ExistsSaleID(guid))
                {
                    SoldCar? car = await _context.SoldCars.FindAsync(guid);

                    car.Car_ID = carData.Car_ID;
                    car.License_Plate = carData.License_Plate;
                    car.Sales_Manager_ID = carData.Sales_Manager_ID;
                    car.Customer_ID = carData.Customer_ID;
                    car.Sale_Date = carData.Sale_Date;
                    car.Sale_Price = carData.Sale_Price;
                    car.Payment_Method = carData.Payment_Method;
                    car.Warranty_Months = carData.Warranty_Months;
                    car.Trade_In = carData.Trade_In;
                    car.Sale_Manager_Image = carData.Sale_Manager_Image;
                    car.Car_Image = carData.Car_Image;

                    _context.SoldCars.Update(car);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"The Sale {guid} has been updated successfully.");
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


        #region Delete By Sale ID
        public async Task<string> DeleteBySaleID(Guid guid)
        {
            try
            {
                if (ExistsSaleID(guid))
                {
                    SoldCar? sale = await _context.SoldCars.FindAsync(guid);
                    _context.SoldCars.Remove(sale);
                    _context.SaveChanges();
                    _logger.LogInformation($"The Sale {guid} has been deleted successfully.");

                    return $"The Sale {guid} has been deleted successfully.";
                }
                else
                {
                    _logger.LogWarning($"Can not delete the Sale {guid}");

                    return $"Can not delete the Sale {guid}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the Sale {guid}.");
                return $"Error occurred while deleting the Sale {guid}.";
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
                    SoldCar? car = await _context.SoldCars
                     .FirstOrDefaultAsync(c => c.License_Plate == plate);

                    _context.SoldCars.Remove(car);
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


        #region Exists Sale Guid
        public bool ExistsSaleID(Guid guid)
        {
            if (_context.SoldCars.Any(c => c.Sale_ID == guid))
            {
                _logger.LogInformation("The Sale Exists!");
                return true;
            }
            else
            {
                _logger.LogInformation("The Sale Does Not Exists.");
                return false;
            }
        }
        #endregion


        #region Exists Car Guid
        public bool ExistsCarID(Guid guid)
        {
            if (_context.SoldCars.Any(c => c.Car_ID == guid))
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
            if (_context.SoldCars.Any(c => c.License_Plate == plate))
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


        #region Exists Sales Manager Guid
        public bool ExistsSalesManagerID(Guid guid)
        {
            if (_context.SoldCars.Any(c => c.Sales_Manager_ID == guid))
            {
                _logger.LogInformation("The Sales Manager Exists!");
                return true;
            }
            else
            {
                _logger.LogInformation("The Sales Manager Does Not Exists.");
                return false;
            }
        }
        #endregion


        #region Exists Customer Guid
        public bool ExistsCustomerID(Guid guid)
        {
            if (_context.SoldCars.Any(c => c.Customer_ID == guid))
            {
                _logger.LogInformation("The Customer Exists!");
                return true;
            }
            else
            {
                _logger.LogInformation("The Customer Does Not Exists.");
                return false;
            }
        }
        #endregion
    }
}