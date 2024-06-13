using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers.DTO
{
    public record AvailableCarData(
        string? License_Plate,
        char Segment,
        string Model,
        int Year,
        string Engine,
        string Transmission,
        int Mileage,
        string Fuel_Economy,
        string Key_Features,
        string Paint_Color,
        string Interior_Color,
        string? Accessories,
        decimal Cost,
        DateTime Arrival_Date,
        Byte[]? Car_Image,
        Byte[]? Intern_Image
        );
    
}
