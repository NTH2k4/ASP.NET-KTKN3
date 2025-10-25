using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _57361.Data;
using _57361.Models;
using X.PagedList;

namespace _57361.Controllers
{
    public class SpeakersController : Controller
    {
        private readonly _57361Context _context;

        public SpeakersController(_57361Context context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1, int pageSize = 3)
        {
            var speakers = _context.Speaker.Select(s => new SpeakerViewModel
            {
                SpeakerId = s.SpeakerId,
                Name = s.Name,
                Title = s.Title,
                Bio = s.Bio
            });
            if (!string.IsNullOrEmpty(searchString))
            {
                speakers = speakers.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["TitleSort"] = sortOrder == "Title" ? "title_desc" : "Title";
            speakers = sortOrder switch
            {
                "name_desc" => speakers.OrderByDescending(s => s.Name),
                "title_desc" => speakers.OrderByDescending(s => s.Title),
                "Title" => speakers.OrderBy(s => s.Title),
                _ => speakers.OrderBy(s => s.Name),
            };
            ViewData["CurrentFilter"] = searchString;
            return View(speakers.ToPagedList(page, pageSize));
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .FirstOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }
            speaker.Presentations = _context.Presentation.Where(p => p.SpeakerId == id).ToList();
            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpeakerId,Name,Title,Bio")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpeakerId,Name,Title,Bio")] Speaker speaker)
        {
            if (id != speaker.SpeakerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.SpeakerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speaker
                .FirstOrDefaultAsync(m => m.SpeakerId == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var speaker = await _context.Speaker.FindAsync(id);
            if (speaker != null)
            {
                _context.Speaker.Remove(speaker);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(int id)
        {
            return _context.Speaker.Any(e => e.SpeakerId == id);
        }
    }
}
