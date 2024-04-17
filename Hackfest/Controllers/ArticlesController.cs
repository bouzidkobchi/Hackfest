using Hackfest.Data;
using Hackfest.Models;
using Hackfest.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Hackfest.Controllers
{
    [Route("/api/articles")]
    public class ArticlesController : Controller
    {
        private readonly AppDbContext _context;

        public ArticlesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/articles/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var activity = _context.Articles.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // GET api/articles
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Articles.ToList());
        }

        // POST api/articles
        [HttpPost]
        public IActionResult Post([FromBody] AddArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var article = new Article
            {
                Name = model.Name,
                Description = model.Description,
                Pictures = model.Pictures.Select(p => p.FileName).ToArray()
            };

            _context.Articles.Add(article);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }

        //// PUT api/articles/{id}
        //[HttpPut("{id}")]
        //public IActionResult Put(string id, [FromBody] AddArticleModel model)
        //{
        //    var article = _context.Articles.Find(id);
        //    if (article == null)
        //    {
        //        return NotFound();
        //    }

        //    article.Name = model.Name;
        //    article.Description = model.Description;
        //    article.Pictures = model.Pictures.Select(picture =>
        //    {
        //        picture.s
        //        return p => p.FileName;
        //    }).ToArray();

        //    _context.SaveChanges();

        //    return NoContent();
        //}

        // PUT api/articles/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AddArticleModel model, [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var article = _context.Articles.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            // Ensure the wwwroot folder exists
            var wwwrootPath = Path.Combine(webHostEnvironment.WebRootPath, "images");
            if (!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath);
            }

            // Process pictures
            var savedPictureAddresses = model.Pictures.Select(picture =>
            {
                // Generate unique filename with datetime stamp
                var fileName = $"IMG_{DateTime.Now:yyyyMMddHHmmssfff}.jpg";
                var filePath = Path.Combine(wwwrootPath, fileName);

                // Save picture to wwwroot folder
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    picture.CopyTo(fileStream);
                }

                return $"/images/{fileName}"; // Return the relative path

            }).ToList();

            // Update article with picture addresses
            article.Name = model.Name;
            article.Description = model.Description;
            article.Pictures = savedPictureAddresses.ToArray();

            _context.SaveChanges();

            // Return the addresses of the saved images
            return Ok(savedPictureAddresses);
        }

        // DELETE api/articles/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var article = _context.Activities.Find(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(article);
            _context.SaveChanges();

            return NoContent();
        }

    }

    //public static class SaveFilesExtension()
    //{
    //    public static IEnumerable<string> SaveAndReturnUrl(this IFormFile[] files)
    //    {
    //        yield return "";
    //    }
    //}
}
