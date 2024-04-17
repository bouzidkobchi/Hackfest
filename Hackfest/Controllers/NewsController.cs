using Hackfest.Data;
using Hackfest.Models;
using Hackfest.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Hackfest.Controllers
{
    [Route("/api/News")]
    public class NewsController : Controller
    {
        private readonly AppDbContext _context;

        public NewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/news/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var newsItem = _context.News.Find(id);
            if (newsItem == null)
            {
                return NotFound();
            }
            return Ok(newsItem);
        }

        // GET api/news
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.News.ToList());
        }

        // POST api/news
        [HttpPost]
        public IActionResult Post([FromBody] AddNewsModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newsItem = new News
            {
                Name = model.Name,
                Description = model.Description,
                Pictures = model.Pictures.Select(p => p.FileName).ToArray()
            };

            _context.News.Add(newsItem);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = newsItem.Id }, newsItem);
        }

        // PUT api/news/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AddActivityModel model)
        {
            var newsItem = _context.News.Find(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            newsItem.Name = model.Name;
            newsItem.Description = model.Description;
            newsItem.Pictures = model.Pictures.Select(p => p.FileName).ToArray();

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/news/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var newsItem = _context.News.Find(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            _context.News.Remove(newsItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
