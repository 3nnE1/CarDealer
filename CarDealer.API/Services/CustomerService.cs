using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarDealer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CarDealerContext _context;
        private readonly ILogger<CustomerService> _logger;


        public CustomerService(CarDealerContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }
   

        public async Task<Customer> GetByID(Guid customerID)
        {
            try
            {
                Customer customer = await _context.Customers.FindAsync(customerID);

                if (customer == null)
                {
                    _logger.LogWarning($"Customer: {customerID}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Customer: {customerID}, has been retrieved successfully.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the Customer with ID: {customerID}.");
                return null;
            }
        }

        public async Task<Customer> GetByTIN(string tin)
        {
            try
            {
                Customer customer = await _context.Customers.FindAsync(tin);

                if (customer == null)
                {
                    _logger.LogWarning($"Customer: {tin}, not found.");
                }
                else
                {
                    _logger.LogInformation($"Customer: {tin}, has been retrieved successfully.");
                }

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the Customer with ID: {tin}.");
                return null;
            }
        }

        public async Task<List<Customer>> GetByFirstName(string firstName)
        {
            try
            {
                _logger.LogInformation($"The Customers named: {firstName}, have been found.");
                return await _context.Customers
                    .Where(a => a.First_Name.ToLower() == firstName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Customers named: {firstName}");
                return null;
            }
        }

        public async Task<List<Customer>> GetByMiddleName(string middleName)
        {
            try
            {
                _logger.LogInformation($"The Customers named: {middleName}, have been found.");
                return await _context.Customers
                    .Where(a => a.Middle_Name.ToLower() == middleName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Customers named: {middleName}");
                return null;
            }
        }


        public async Task<List<Customer>> GetByLastName(string lastName)
        {
            try
            {
                _logger.LogInformation($"The Customers named: {lastName}, have been found.");
                return await _context.Customers
                    .Where(a => a.Last_Name.ToLower() == lastName.ToLower())
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured while finding the Customers named: {lastName}");
                return null;
            }
        }


        public async Task<bool> Add(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Customer: {customer} added successfully.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding a Customer: {customer}");
                return false;
            }
        }


        public async Task<Customer> Edit(Guid guid, CustomerData customerData)
        {
            try
            {
                Customer? customer = await _context.Customers.FindAsync(guid);

                if (customer == null)
                {
                    _logger.LogWarning($"The Customer: {guid}, is not found");
                    return null;
                }

                else
                {
                    customer.TIN = customerData.TIN;
                    customer.First_Name = customerData.First_Name;
                    customer.Middle_Name = customerData.Middle_Name;
                    customer.Last_Name = customerData.Last_Name;
                    customer.Date_Of_Birth = customerData.Date_Of_Birth;
                    customer.Phone_Number = customerData.Phone_Number;
                    customer.Email = customerData.Email;
                    customer.Address = customerData.Address;
                    customer.City = customerData.City;
                    customer.State = customerData.State;
                    customer.Zip_Code = customerData.Zip_Code;
                    customer.Country = customerData.Country;
                    customer.Registration_Date = customerData.Registration_Date;

                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();

                    _logger.LogInformation($"The Customer: {guid}, has been updated successfully.");
                    return customer;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Customer: {guid}.");
                return null;
            }
        }


        public async Task<bool> Remove(Customer customer)
        {
            _logger.LogInformation($"{customer.Customer_ID} is not more available.");
            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
