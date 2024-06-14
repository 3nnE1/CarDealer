using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers.DTO
{
    public record SalesManagerData(
        string TIN,
        string First_Name,
        string? Middle_Name,
        string Last_Name,
        DateTime Date_Of_Birth,
        string Phone_Number,
        string Email,
        DateTime Hire_Date,
        string Address_Line,
        string City,
        string State,
        string ZipCode,
        string Country
        );
}
