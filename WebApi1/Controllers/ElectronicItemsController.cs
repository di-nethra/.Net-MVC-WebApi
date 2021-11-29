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
using WebApi1.Models;

namespace WebApi1.Controllers
{
    public class ElectronicItemsController : ApiController
    {
        private ElectronicsEntities1 db = new ElectronicsEntities1();

        // GET: api/ElectronicItems
        public IQueryable<ElectronicItem> GetElectronicItems()
        {
            return db.ElectronicItems;
        }

        // GET: api/ElectronicItems/5
        [ResponseType(typeof(ElectronicItem))]
        public IHttpActionResult GetElectronicItem(int id)
        {
            ElectronicItem electronicItem = db.ElectronicItems.Find(id);
            if (electronicItem == null)
            {
                return NotFound();
            }

            return Ok(electronicItem);
        }

        // PUT: api/ElectronicItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutElectronicItem(int id, ElectronicItem electronicItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != electronicItem.ID)
            {
                return BadRequest();
            }

            db.Entry(electronicItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectronicItemExists(id))
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

        // POST: api/ElectronicItems
        [ResponseType(typeof(ElectronicItem))]
        public IHttpActionResult PostElectronicItem(ElectronicItem electronicItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ElectronicItems.Add(electronicItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = electronicItem.ID }, electronicItem);
        }

        // DELETE: api/ElectronicItems/5
        [ResponseType(typeof(ElectronicItem))]
        public IHttpActionResult DeleteElectronicItem(int id)
        {
            ElectronicItem electronicItem = db.ElectronicItems.Find(id);
            if (electronicItem == null)
            {
                return NotFound();
            }

            db.ElectronicItems.Remove(electronicItem);
            db.SaveChanges();

            return Ok(electronicItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ElectronicItemExists(int id)
        {
            return db.ElectronicItems.Count(e => e.ID == id) > 0;
        }
    }
}