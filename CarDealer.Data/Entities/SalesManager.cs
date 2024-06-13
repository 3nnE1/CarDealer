using System.ComponentModel.DataAnnotations;

namespace CarDealer.Data.Entities
{
    public sealed class SalesManager
    {
        [Key] public Guid Sales_Manager_ID { get; set; }
        public string TIN { get; set; } = string.Empty;
        public string First_Name { get; set; } = string.Empty;
        public string? Middle_Name { get; set; }
        public string Last_Name { get; set; } = string.Empty;
        public DateTime Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime Hire_Date { get; set; }
        public string Address_Line { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
