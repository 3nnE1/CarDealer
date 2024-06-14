using System.ComponentModel.DataAnnotations;

namespace CarDealer.Data.Entities
{
    public sealed class Customer
    {
        [Key] public Guid Customer_ID { get; set; }
        public string TIN { get; set; } = string.Empty;
        public string First_Name { get; set; } = string.Empty;
        public string? Middle_Name { get; set; }
        public string Last_Name { get; set; } = string.Empty;
        public DateTime Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip_Code { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime Registration_Date { get; set;}
    }
}
