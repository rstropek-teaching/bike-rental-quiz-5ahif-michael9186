using System;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Model
{
    public class Rental
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public Bike Bike { get; set; }

        [Required]
        public DateTime RentalBegin { get; set; }

        [Required]
        public DateTime? RentalEnd { get; set; }

        [Range(1.00, Double.MaxValue)]
        public Double RentalCosts { get; set; }

        [Required]
        public Boolean Paid { get; set; }
    }
}
