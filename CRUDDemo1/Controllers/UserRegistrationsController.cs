using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRUDDemo1.Models;

namespace CRUDDemo1.Controllers
{
    public class UserRegistrationsController : ApiController
    {
        private PallaviEntities1 db = new PallaviEntities1();

        // GET: api/UserRegistrations
        public IQueryable<UserRegistration> GetUserRegistrations()
        {
            return db.UserRegistrations;
        }

        // GET: api/UserRegistrations/5
        [ResponseType(typeof(UserRegistration))]
        public IHttpActionResult GetUserRegistration(int id)
        {
            UserRegistration userRegistration = db.UserRegistrations.Find(id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            return Ok(userRegistration);
        }

        // PUT: api/UserRegistrations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserRegistration(int id, UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userRegistration.Userid)
            {
                return BadRequest();
            }

            db.Entry(userRegistration).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserRegistrationExists(id))
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

        // POST: api/UserRegistrations
        [ResponseType(typeof(UserRegistration))]
        public IHttpActionResult PostUserRegistration(UserRegistration userRegistration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserRegistrations.Add(userRegistration);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userRegistration.Userid }, userRegistration);
        }

        // DELETE: api/UserRegistrations/5
        [ResponseType(typeof(UserRegistration))]
        public IHttpActionResult DeleteUserRegistration(int id)
        {
            UserRegistration userRegistration = db.UserRegistrations.Find(id);
            if (userRegistration == null)
            {
                return NotFound();
            }

            db.UserRegistrations.Remove(userRegistration);
            db.SaveChanges();

            return Ok(userRegistration);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserRegistrationExists(int id)
        {
            return db.UserRegistrations.Count(e => e.Userid == id) > 0;
        }
    }
}