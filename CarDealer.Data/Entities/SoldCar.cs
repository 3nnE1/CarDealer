using System.ComponentModel.DataAnnotations;

namespace CarDealer.Data.Entities
{
    public sealed class SoldCar
    {
        [Key] public Guid Sale_ID { get; set; }
        public Guid Car_ID { get; set; }
        public Guid Sales_Manager_ID { get; set; }
        public Guid Customer_ID { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
        public string Payment_Method { get; set; } = string.Empty;
        public byte Warranty_Months { get; set; }
        public bool Trade_In { get; set; }
        public Byte[]? Sale_Manager_Image { get; set; }
        public Byte[]? Car_Image { get; set; }
    }
}
