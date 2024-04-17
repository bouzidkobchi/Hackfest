using Hackfest.Data;
using Hackfest.Models;
using Hackfest.Models.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Hackfest.Controllers
{
    // get(string id)
    // getAll
    // post(AddActivityModel)
    // put(AddActivityModel)
    // delete(string Id)

    [Route("/api/Activities")]
    public class ActivitiesController : Controller
    {
        private readonly AppDbContext _context;

        public ActivitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET api/activities/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        // GET api/activities
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Activities.ToList());
        }

        // POST api/activities
        [HttpPost]
        public IActionResult Post([FromBody] AddActivityModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = new Activity
            {
                Name = model.Name,
                Description = model.Description,
                Pictures = model.Pictures.Select(p => p.FileName).ToArray()
            };

            _context.Activities.Add(activity);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = activity.Id }, activity);
        }

        // PUT api/activities/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AddActivityModel model)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            activity.Name = model.Name;
            activity.Description = model.Description;
            activity.Pictures = model.Pictures.Select(p => p.FileName).ToArray();

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/activities/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var activity = _context.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
