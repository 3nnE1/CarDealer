using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers.DTO
{
    public record SoldCarData(
        Guid Car_ID,
        string License_Plate,
        Guid Sales_Manager_ID,
        Guid Customer_ID,
        DateTime Sale_Date,
        decimal Sale_Price,
        string Payment_Method,
        byte Warranty_Months,
        bool Trade_In,
        Byte[]? Sale_Manager_Image,
        Byte[]? Car_Image
        );
}
