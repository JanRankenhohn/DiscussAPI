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
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;

namespace Discuss_API.Controllers
{
    public class ArticlesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Articles
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        public IQueryable<Articles> GetArticles()
        {
            return db.Articles;
        }

        // GET: api/Articles/5
        /// <summary>
        /// TETS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Articles))]
        public async Task<IHttpActionResult> GetArticles(int id)
        {
            Articles articles = await db.Articles.FindAsync(id);
            if (articles == null)
            {
                return NotFound();
            }

            return Ok(articles);
        }

        // GET: api/Articles/5
        [HttpGet]
        [ResponseType(typeof(Articles))]
        public async Task<IHttpActionResult> GetArticlesForDiscussion(int discussion_Id)
        {
            try
            {
                ICollection<Articles> articles = db.Discussions.Find(discussion_Id).Articles;
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // PUT: api/Articles/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArticles(Articles article)
        {

            var oldArticle = db.Articles.Find(article.Id);

            if (oldArticle == null)
            {
                return BadRequest();
            }

            oldArticle.Name = article.Name;
            oldArticle.Content = article.Content;
            oldArticle.Discussion = await db.Discussions.FindAsync(article.Discussion_Id);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!ArticlesExists(article.Id))
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

        // POST: api/Articles
        [HttpPost]
        [ResponseType(typeof(Articles))]
        public async Task<IHttpActionResult> PostArticle(Articles article)
        {
            article.Discussion = db.Discussions.Find(article.Discussion_Id);

            db.Articles.Add(article);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = article.Id }, article);
        }

        //// DELETE: api/Articles/5
        //[HttpPost]
        //[Route("api/deleteArticle/{id}")]
        //[ResponseType(typeof(Articles))]
        //public async Task<IHttpActionResult> DeleteArticle(int id)
        //{
        //    Articles article = await db.Articles.FindAsync(id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Articles.Remove(article);
        //    await db.SaveChangesAsync();

        //    return Ok(article);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticlesExists(int id)
        {
            return db.Articles.Count(e => e.Id == id) > 0;
        }
    }
}