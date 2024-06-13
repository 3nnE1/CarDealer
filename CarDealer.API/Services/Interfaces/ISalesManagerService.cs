using CarDealer.Controllers.DTO;
using CarDealer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Services.Interfaces
{
    internal interface ISalesManagerService
    {
        Task<SalesManager> GetByID(Guid salesManagerID);
        Task<SalesManager> GetByTIN(string tin);
        Task<List<SalesManager>> GetByFirstName(string firstName);
        Task<List<SalesManager>> GetByMiddleName(string middleName);
        Task<List<SalesManager>> GetByLastName(string lastName);
        Task<bool> Add(SalesManager salesManager);
        Task<bool> Remove(SalesManager salesManager);
        Task<SalesManager> Edit(Guid guid, SalesManagerData salesManagerData);

    }
}
