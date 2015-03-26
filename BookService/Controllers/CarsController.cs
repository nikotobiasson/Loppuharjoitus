using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CarService.Models;

namespace CarService.Controllers
{
    public class CarsController : ApiController
    {
        private CarServiceContext db = new CarServiceContext();

        // GET api/Cars
        public IQueryable<CarDTO> GetCars()
        {
            var cars = from b in db.Cars
                        select new CarDTO()
                        {
                            Id = b.Id,
                            Title = b.Title,
                            OwnerName = b.Owner.Name
                        };

            return cars;
        }

        // GET api/Cars/5
        [ResponseType(typeof(CarDetailDTO))]
        public async Task<IHttpActionResult> GetCar(int id)
        {
            var car = await db.Cars.Include(b => b.Owner).Select(b =>
                new CarDetailDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Price = b.Price,
                    OwnerName = b.Owner.Name
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        } 

        // PUT: api/Cars/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCar(int id, Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != car.Id)
            {
                return BadRequest();
            }

            db.Entry(car).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cars
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> PostCar(Car car)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cars.Add(car);
            await db.SaveChangesAsync();

            // New code:
            // Load owner name
            db.Entry(car).Reference(x => x.Owner).Load();

            var dto = new CarDTO()
            {
                Id = car.Id,
                Title = car.Title,
                OwnerName = car.Owner.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = car.Id }, dto);
        }

        // DELETE: api/Cars/5
        [ResponseType(typeof(Car))]
        public async Task<IHttpActionResult> DeleteCar(int id)
        {
            Car car = await db.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            db.Cars.Remove(car);
            await db.SaveChangesAsync();

            return Ok(car);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarExists(int id)
        {
            return db.Cars.Count(e => e.Id == id) > 0;
        }
    }
}