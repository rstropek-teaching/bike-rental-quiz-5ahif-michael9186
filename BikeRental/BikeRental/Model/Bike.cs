using System;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Model
{
    public enum Category
    {
        Standardbike, Mountainbike, Treckingbike, Racingbike
    }

    public class Bike
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; }

        [MaxLength(1000)]
        public String Notes { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfLastService { get; set; }

        [Required]
        [Range(0.00, Double.MaxValue)]
        public Double RentalPriceFirstHour { get; set; }

        [Required]
        [Range(1.00, Double.MaxValue)]
        public Double RentalPriceAdditionalHour { get; set; }

        [Required]
        public Category BikeCategory { get; set; }
    }
}
