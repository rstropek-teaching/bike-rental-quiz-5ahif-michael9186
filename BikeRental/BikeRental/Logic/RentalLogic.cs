using BikeRental.Model;
using System;

namespace BikeRental.Logic
{
    public class RentalLogic
    {
        public double Calculate(Rental rent)
        {
            TimeSpan t = (DateTime)rent.RentalEnd - rent.RentalBegin;
            double min = t.TotalMinutes;
            double price = 0.00d;

            if (min <= 15)
            {
                // free
                return price;
            }
            else
            {
                // pay
                price += rent.Bike.RentalPriceFirstHour;
                min -= 60;

                while (min > 0)
                {
                    price += rent.Bike.RentalPriceAdditionalHour;
                    min -= 60;
                }
                return price;
            }
        }
    }
}
