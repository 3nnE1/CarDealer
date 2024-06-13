using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Controllers.DTO
{
    public record SoldCarData(
        Guid Sale_ID,
        Guid Car_ID,
        Guid Sales_Manager_ID,
        Guid Customer_ID,
        DateTime SaleDate,
        decimal SalePrice,
        string Payment_Method,
        byte Warranty_Months,
        bool Trade_In,
        Byte[]? Sale_Manager_Image,
        Byte[]? Car_Image
        );
}
