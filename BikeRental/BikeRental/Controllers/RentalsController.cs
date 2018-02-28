using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Context;
using BikeRental.Model;
using BikeRental.Logic;

namespace BikeRental.Controllers
{
    [Route("api/rentals")]
    public class RentalsController : Controller
    {
        private readonly DataContext db = new DataContext();
        private RentalLogic logic = new RentalLogic();

        [HttpGet]
        public IActionResult GetRentals()
        {
            return Ok(db.Rentals);
        }

        [HttpGet("{id}")]
        public IActionResult GetRental([FromRoute] int id)
        {
            var rental = db.Rentals.SingleOrDefault(r => r.ID == id);

            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        [HttpPost]
        public IActionResult AddRental([FromBody] Rental rental)
        {
            db.Rentals.Add(rental);
            db.SaveChanges();

            return Ok("Added Rental");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteRental([FromRoute] int id)
        {
            var rental = db.Rentals.SingleOrDefault(r => r.ID == id);

            if (rental == null)
            {
                return NotFound();
            }

            db.Rentals.Remove(rental);
            db.SaveChanges();

            return Ok("deleted");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRental([FromRoute] int id, [FromBody] Rental rental)
        {
            if (id != rental.ID)
            {
                return BadRequest();
            }

            var oldRental = db.Rentals.SingleOrDefault(r => r.ID == id);
            db.Entry(oldRental).CurrentValues.SetValues(rental);
            db.SaveChanges();

            return Ok("updated");
        }

        [HttpGet]
        [Route("StartRental")]
        public IActionResult StartRental(int custId, int bikeId)
        {
            if (!db.Customers.Any(cst => cst.ID == custId) || !db.Bikes.Any(bk => bk.ID == bikeId))
            {
                return BadRequest("No valid IDs");
            }

            Rental newRental = new Rental()
            {
                Customer = db.Customers.First(cst => cst.ID == custId),
                Bike = db.Bikes.First(bk => bk.ID == bikeId),
                RentalBegin = DateTime.Now,
                RentalEnd = null,
                Paid = false,
                RentalCosts = 0
            };

            db.Rentals.Add(newRental);
            db.SaveChanges();

            return Ok("Started: \n" + newRental);
        }

        [HttpGet]
        [Route("EndRental")]
        public IActionResult EndRental(int custId, int bikeId)
        {
            double price = 0.00d;

            var rent = db.Rentals.First(rnt => rnt.Customer.ID == custId & rnt.Bike.ID == bikeId);
            rent.RentalEnd = DateTime.Now;

            price = logic.Calculate(rent);
            rent.RentalCosts = price;

            db.SaveChanges();

            return Ok(rent);
        }

        [HttpGet]
        [Route("Pay")]
        public IActionResult MarkPaid(int custId, int bikeID)
        {
            var rent = db.Rentals.First(rnt => rnt.Customer.ID == custId & rnt.Bike.ID == bikeID);

            if (rent.RentalCosts > 0)
            {
                rent.Paid = true;
            }
            else
            {
                return BadRequest("Customer has not paid");
            }
            db.SaveChanges();

            return Ok("Customer (id=" + custId + "), Bike (id=" + bikeID + ") Paid");
        }

        [HttpGet]
        [Route("Unpaid")]
        public IActionResult GetUnpaid()
        {
            return Ok(db.Rentals.Where(r => r.Paid == false && r.RentalEnd > r.RentalBegin && r.RentalEnd != null && r.RentalCosts > 0).
                SelectMany(rntobj => new Object[] {
                    rntobj.Customer.ID,
                    rntobj.Customer.FirstName,
                    rntobj.Customer.LastName,
                    rntobj.ID,
                    rntobj.RentalBegin,
                    rntobj.RentalEnd,
                    rntobj.RentalCosts }
                ));
            
        }




    }
}
