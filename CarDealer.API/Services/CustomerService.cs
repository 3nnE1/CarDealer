using CarDealer.Controllers.DTO;
using CarDealer.Data;
using CarDealer.Data.Entities;
using CarDealer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace CarDealer.Services
{
    public class CustomerService : ICustomerService
    {
        #region Setup
        private readonly CarDealerContext _context;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(CarDealerContext context, ILogger<CustomerService> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion


        #region Get By ID
        public async Task<Customer> GetByID(Guid guid)
        {
            try
            {
                if (ExistsByID(guid))
                {
                    Customer? customer = await _context.Customers.FindAsync(guid);
                    _logger.LogInformation($"Customer: {guid}, has been retrieved successfully.");
                    return customer;
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

       
        #region Get By TIN
        public async Task<Customer> GetByTIN(string tin)
        {
            try
            {
                if (ExistsTIN(tin))
                {
                    Customer? customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.TIN == tin);

                    _logger.LogInformation($"Customer: {tin}, has been retrieved successfully.");
                    return customer;
                }
                else
                {
                    _logger.LogWarning($"Customer: {tin}, not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting car with plate: {tin}.");
                return null;
            }
        }
        #endregion


        #region Get By First Name
        public async Task<List<Customer>> GetByFirstName(string name)
        {
            try
            {
                List<Customer> customersList = await _context.Customers
                .Where(a => a.First_Name == name)
                .ToListAsync();

                _logger.LogInformation($"Here is the Customer List named {name}");
                return customersList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Customers named {name}.");
                return null;
            }
        }
        #endregion


        #region Get By Middle Name
        public async Task<List<Customer>> GetByMiddleName(string name)
        {
            try
            {
                List<Customer> customersList = await _context.Customers
                .Where(a => a.Middle_Name == name)
                .ToListAsync();

                _logger.LogInformation($"Here is the Customer List named {name}");
                return customersList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Customers named {name}.");
                return null;
            }
        }
        #endregion


        #region Get By Last Name
        public async Task<List<Customer>> GetByLastName(string name)
        {
            try
            {
                List<Customer> customersList = await _context.Customers
                .Where(a => a.Last_Name == name)
                .ToListAsync();

                _logger.LogInformation($"Here is the Customer List named {name}");
                return customersList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting Customers named {name}.");
                return null;
            }
        }
        #endregion


        #region Add Customer
        public async Task<Customer> AddCustomer(CustomerData customerData)
        {
            try
            {
                Customer customer = new Customer
                {
                    TIN = customerData.TIN,
                    First_Name = customerData.First_Name,
                    Middle_Name = customerData.Middle_Name,
                    Last_Name = customerData.Last_Name,
                    Date_Of_Birth = customerData.Date_Of_Birth,
                    Phone_Number = customerData.Phone_Number,
                    Email = customerData.Email,
                    Address = customerData.Address,
                    City = customerData.City,
                    State = customerData.State,
                    Zip_Code = customerData.Zip_Code,
                    Country = customerData.Country,
                    Registration_Date = customerData.Registration_Date
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Customer added successfully: {customer}");
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while adding the Customer: {customerData}");
                return null;
            }
        }
        #endregion


        #region Edit By Customer ID
        public async Task<Customer> EditByID(Guid guid, CustomerData customerData)
        {
            try
            {
                if (ExistsByID(guid))
                {
                    Customer? customer = await _context.Customers.FindAsync(guid);

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

                    _logger.LogInformation($"The Customer {guid} has been updated successfully.");
                    return customer;
                }
                else
                {
                    _logger.LogWarning($"Can not update the Customer {guid}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating the Customer {guid}.");
                return null;
            }
        }
        #endregion


        #region Delete By Customer ID
        public async Task<string> DeleteByID(Guid guid)
        {
            try
            {
                if (ExistsByID(guid))
                {
                    Customer? customer = await _context.Customers.FindAsync(guid);
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    _logger.LogInformation($"The Customer {customer} has been deleted successfully.");

                    return $"The Customer {customer} has been deleted successfully.";
                }
                else
                {
                    _logger.LogWarning($"Can not delete the Customer {guid}");

                    return $"Can not delete the Customer {guid}";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting the Customer {guid}.");
                return $"Error occurred while deleting the Customer {guid}.";
            }
        }
        #endregion


        #region Exists By ID
        public bool ExistsByID(Guid guid)
        {
            if (_context.Customers.Any(c => c.Customer_ID == guid))
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


        #region Exists TIN
        public bool ExistsTIN(string tin)
        {
            if (_context.Customers.Any(c => c.TIN == tin))
            {
                _logger.LogInformation("The Customer Exists.");
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