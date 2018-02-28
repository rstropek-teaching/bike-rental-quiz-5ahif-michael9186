using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Context;
using BikeRental.Model;

namespace BikeRental.Controllers
{
    [Route("api/bikes")]
    public class BikesController : Controller
    {
        private readonly DataContext db = new DataContext();

        [HttpGet]
        public IActionResult GetBikes()
        {
            return Ok(db.Bikes);
        }

        [HttpGet]
        [Route("GetAvailableBikes")]
        public IActionResult GetAvailableBikes([FromQuery] string sort)
        {
            if (sort.Equals("purchase"))
            {
                return Ok(db.Bikes.Where(b => !db.Rentals.Any(r => r.Bike.ID == b.ID & r.RentalBegin > r.RentalEnd))
                    .OrderByDescending(b => b.PurchaseDate));

            }else if (sort.Equals("firstHour"))
            {
                return Ok(db.Bikes.Where(b => !db.Rentals.Any(r => r.Bike.ID == b.ID & r.RentalBegin > r.RentalEnd))
                    .OrderBy(b => b.RentalPriceFirstHour));

            }else if (sort.Equals("addHour"))
            {
                return Ok(db.Bikes.Where(b => !db.Rentals.Any(r => r.Bike.ID == b.ID & r.RentalBegin > r.RentalEnd))
                    .OrderBy(b => b.RentalPriceAdditionalHour));

            }
            else
            {
                return Ok(db.Bikes.Where(b => !db.Rentals.Any(r => r.Bike.ID == b.ID & r.RentalBegin > r.RentalEnd)));
            }          
        }

        [HttpGet("{id}")]
        public IActionResult GetBike([FromRoute] int id)
        {
            var bike = db.Bikes.SingleOrDefault(b => b.ID == id);

            if (bike == null)
            {
                return NotFound();
            }

            return Ok(bike);
        }

        [HttpPost]
        public IActionResult AddBike([FromBody] Bike bike)
        {
            db.Bikes.Add(bike);
            db.SaveChanges();

            return Ok("Added Bike");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBike([FromRoute] int id)
        {
            var bike = db.Bikes.SingleOrDefault(b => b.ID == id);

            if (bike == null)
            {
                return NotFound();
            }

            if (db.Rentals.Any(bk => bk.Bike.ID == id))
            {
                return BadRequest("Bike is in use!");
            }

            db.Bikes.Remove(bike);
            db.SaveChanges();

            return Ok("deleted");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBike([FromRoute] int id, [FromBody] Bike bike)
        {
            if (id != bike.ID)
            {
                return BadRequest();
            }

            var oldBike = db.Bikes.SingleOrDefault(b => b.ID == id);
            db.Entry(oldBike).CurrentValues.SetValues(bike);
            db.SaveChanges();

            return Ok("updated");
        }
    }
}