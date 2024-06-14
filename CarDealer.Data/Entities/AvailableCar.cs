using System.ComponentModel.DataAnnotations;

namespace CarDealer.Data.Entities
{
    public sealed class AvailableCar
    {
        [Key] public Guid Car_ID { get; set; }
        public string License_Plate { get; set; }
        public char Segment { get; set; }
        public string Model { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Engine { get; set; } = string.Empty;
        public string Transmission { get; set; } = string.Empty;
        public int Mileage { get; set; }
        public string Fuel_Economy { get; set; } = string.Empty;
        public string Key_Features { get; set; } = string.Empty;
        public string Paint_Color { get; set; } = string.Empty;
        public string Interior_Color { get; set; } = string.Empty;
        public string? Accessories { get; set; }
        public decimal Cost { get; set; }
        public DateTime Arrival_Date { get; set; }
        public Byte[]? Car_Image { get; set; }
        public Byte[]? Intern_Image { get; set; }
    }
}
