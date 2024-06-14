using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers.DTO
{
    public record CustomerData(
        string TIN,
        string First_Name,
        string? Middle_Name,
        string Last_Name,
        DateTime Date_Of_Birth,
        string Phone_Number,
        string Email,
        string Address,
        string City,
        string State,
        string Zip_Code,
        string Country,
        DateTime Registration_Date
        );
}
