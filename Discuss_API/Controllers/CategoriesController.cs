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
using Discuss_API.Models;

namespace Discuss_API.Controllers
{
    public class CategoriesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Categories
        public IQueryable<Categories> GetCategories()
        {
            return db.Categories;
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Categories))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            Categories category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(int id, Categories category)
        {
            var oldCategory = db.Discussions.Find(category.Id);

            if (category == null)
            {
                return BadRequest();
            }

            oldCategory.Name = category.Name;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!CategoriesExists(category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        // POST: api/Categories
        [ResponseType(typeof(Categories))]
        public async Task<IHttpActionResult> PostCategory(Categories category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        //[ResponseType(typeof(Categories))]
        //public async Task<IHttpActionResult> DeleteCategories(int id)
        //{
        //    Categories categories = await db.Categories.FindAsync(id);
        //    if (categories == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Categories.Remove(categories);
        //    await db.SaveChangesAsync();

        //    return Ok(categories);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriesExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}