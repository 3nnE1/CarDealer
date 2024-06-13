using CarDealer.Controllers.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealer.Data.Entities;

namespace CarDealer.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> GetByID(Guid customerID);
        Task<Customer> GetByTIN(string tin);
        Task<List<Customer>> GetByFirstName(string firstName);
        Task<List<Customer>> GetByMiddleName(string middleName);
        Task<List<Customer>> GetByLastName(string lastName);
        Task<bool> Add(Customer customer);
        Task<bool> Remove(Customer customer);
        Task<Customer> Edit(Guid guid, CustomerData customerData);
        
    }
}
