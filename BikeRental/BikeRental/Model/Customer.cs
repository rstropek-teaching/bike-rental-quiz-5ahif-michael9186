using System;
using System.ComponentModel.DataAnnotations;

namespace BikeRental.Model
{
    public enum Gender
    {
        male, female, unknown
    }

    public class Customer
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public String LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [MaxLength(10)]
        public String HouseNumber { get; set; }

        [Required]
        [MaxLength(10)]
        public String ZipCode { get; set; }

        [Required]
        [MaxLength(75)]
        public String Town { get; set; }
    }
}
