using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class SalesManagerService : ISalesManagerService
    {
        private readonly CarDealerContext _context;
        private readonly ILogger<SalesManagerService> _logger;

        public SalesManagerService(CarDealerContext context, ILogger<SalesManagerService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<SalesManager> GetByID(Guid salesManagerID)
        {
            try
            {
                SalesManager? salesManager = await _context.SalesManagers.FindAsync(salesManagerID);

                if (salesManager == null)
                {
                    _logger.LogWarning($"Sales Manager: {salesManagerID}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Sales Manager: {salesManagerID}, has been retrieved successfully.");
                }

                return salesManager;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sales Manager: {salesManagerID}.");
                return null;
            }
        }


        public async Task<SalesManager> GetByTIN(string tin)
        {
            try
            {
                SalesManager? salesManager = await _context.SalesManagers.FindAsync(tin);

                if (salesManager == null)
                {
                    _logger.LogWarning($"Sales Manager: {tin}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Sales Manager: {tin}, has been retrieved successfully.");
                }

                return salesManager;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Sales Manager: {tin}.");
                return null;
            }
        }


        public async Task<List<SalesManager>> GetByFirstName(string firstName)
        {
            try
            {
                _logger.LogInformation($"The Sales Manager named: {firstName}, have been found.");
                return await _context.SalesManagers
                    .Where(a => a.First_Name.ToLower() == firstName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Sales Managers named: {firstName}");
                return null;
            }
        }


        public async Task<List<SalesManager>> GetByMiddleName(string middleName)
        {
            try
            {
                _logger.LogInformation($"The Sales Manager named: {middleName}, have been found.");
                return await _context.SalesManagers
                    .Where(a => a.Middle_Name.ToLower() == middleName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Sales Managers named: {middleName}");
                return null;
            }
        }


        public async Task<List<SalesManager>> GetByLastName(string lastName)
        {
            try
            {
                _logger.LogInformation($"The Sales Manager named: {lastName}, have been found.");
                return await _context.SalesManagers
                    .Where(a => a.Last_Name.ToLower() == lastName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Sales Managers named: {lastName}");
                return null;
            }
        }


        public async Task<bool> Add(SalesManager salesManager)
        {
            try
            {
                _context.SalesManagers.Add(salesManager);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Sales Manager added successfully: {salesManager}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a Sales Manager: {salesManager}");
                return false;
            }
        }


        public async Task<SalesManager> Edit(Guid guid, SalesManagerData salesManagerData)
        {
            try
            {
                SalesManager? salesManager = await _context.SalesManagers.FindAsync(guid);

                if (salesManager == null)
                {
                    _logger.LogWarning($"The Sales Manager {guid} is not found");
                    return null;
                }

                else
                {
                    salesManager.TIN = salesManagerData.TIN;
                    salesManager.First_Name = salesManagerData.First_Name;
                    salesManager.Middle_Name = salesManagerData.Middle_Name;
                    salesManager.Last_Name = salesManagerData.Last_Name;
                    salesManager.Date_Of_Birth = salesManagerData.Date_Of_Birth;
                    salesManager.Phone_Number = salesManagerData.Phone_Number;
                    salesManager.Email = salesManagerData.Email;
                    salesManager.Hire_Date = salesManagerData.Hire_Date;
                    salesManager.Address_Line = salesManagerData.Address_Line;
                    salesManager.City = salesManagerData.City;
                    salesManager.State = salesManagerData.State;
                    salesManager.ZipCode = salesManagerData.ZipCode;
                    salesManager.Country = salesManagerData.Country;

                    _context.SalesManagers.Update(salesManager);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"The SalesManager {guid} has been updated successfully.");
                    return salesManager;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Sales Manager: {guid}.");
                return null;
            }
        }


        public async Task<bool> Remove(SalesManager salesManager)
        {
            _logger.LogInformation($"{salesManager.Sales_Manager_ID} is not more available.");
            _context.SalesManagers.Remove(salesManager);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
