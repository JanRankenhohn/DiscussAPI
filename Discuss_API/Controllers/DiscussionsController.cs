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
    public class DiscussionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Discussions
        public IQueryable<Discussions> GetDiscussions()
        {
            return db.Discussions;
        }

        // GET: api/Discussions/5
        [ResponseType(typeof(Discussions))]
        public async Task<IHttpActionResult> GetDiscussion(int id)
        {
            Discussions discussions = await db.Discussions.FindAsync(id);
            if (discussions == null)
            {
                return NotFound();
            }

            return Ok(discussions);
        }

        // PUT: api/Discussions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDiscussion(int id, Discussions discussion)
        {
            var oldDiscussion = db.Discussions.Find(discussion.Id);

            if (discussion == null)
            {
                return BadRequest();
            }

            oldDiscussion.Name = discussion.Name;
            oldDiscussion.Category = await db.Categories.FindAsync(discussion.Category_Id);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!DiscussionsExists(discussion.Id))
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

        // POST: api/Discussions
        [ResponseType(typeof(Discussions))]
        public async Task<IHttpActionResult> PostDiscussions(Discussions discussion)
        {
            discussion.Category = db.Categories.Find(discussion.Category_Id);

            db.Discussions.Add(discussion);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = discussion.Id }, discussion);
        }

        // DELETE: api/Discussions/5
        //[ResponseType(typeof(Discussions))]
        //public async Task<IHttpActionResult> DeleteDiscussions(int id)
        //{
        //    Discussions discussions = await db.Discussions.FindAsync(id);
        //    if (discussions == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Discussions.Remove(discussions);
        //    await db.SaveChangesAsync();

        //    return Ok(discussions);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DiscussionsExists(int id)
        {
            return db.Discussions.Count(e => e.Id == id) > 0;
        }
    }
}