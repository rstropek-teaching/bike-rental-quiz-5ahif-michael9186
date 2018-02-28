using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BikeRental.Context;
using BikeRental.Model;

namespace BikeRental.Controllers
{
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        private readonly DataContext db = new DataContext();

        [HttpGet]
        public IActionResult GetCustomers()
        {
            return Ok(db.Customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer([FromRoute] int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }

            var oldCustomer = db.Customers.SingleOrDefault(c => c.ID == id);
            db.Entry(oldCustomer).CurrentValues.SetValues(customer);
            db.SaveChanges();

            return Ok("updated");
        }

        [HttpPost]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();

            return Ok("Added Customer");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer([FromRoute] int id)
        {
            var customer = db.Customers.SingleOrDefault(c => c.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            if(db.Rentals.Any(r => r.Customer.ID == id))
            {
                return BadRequest("Unable to Delete Customer with Rental");
            }
            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok("deleted");
        }

        [HttpGet]
        [Route("GetCustomerRentals/{id}")]
        public IActionResult GetCustomerRentals(int id)
        {
            if (db.Customers.Any(c => c.ID == id))
            {
                return NotFound("Customer Not Found");
            }
            return Ok(db.Rentals.Where(rnt => rnt.Customer.ID == id));
        }
    }
}