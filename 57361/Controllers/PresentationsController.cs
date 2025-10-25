using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _57361.Data;
using _57361.Models;
using X.PagedList;

namespace _57361.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly _57361Context _context;

        public PresentationsController(_57361Context context)
        {
            _context = context;
        }

        // GET: Presentations
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1, int pageSize = 3)
        {
            var presentations = _context.Presentation.Select(p => new PresentationViewModel
            {
                Duration = p.Duration,
                Slides = p.Slides,
                Topic = p.Topic,
                SpeakerName = p.Speaker.Name,
                PresentationId = p.PresentationId
            });
            if (!string.IsNullOrEmpty(searchString))
            {
                presentations = presentations.Where(p => p.Topic.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            //ViewData["TopictionSort"] = string.IsNullOrEmpty(sortOrder) ? "topic_desc" : "";
            //ViewData["DurationSort"] = sortOrder == "dura" ? "dura_desc" : "dura";
            presentations = sortOrder switch
            {
                "topic_desc" => presentations.OrderByDescending(p => p.Topic),
                "dura_desc" => presentations.OrderByDescending(p => p.Duration),
                "dura" => presentations.OrderBy(p => p.Duration),
                _ => presentations.OrderBy(p => p.Topic)
            };
            return View(presentations.ToPagedList(page, pageSize));
        }

        // GET: Presentations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentation
                .Include(p => p.Speaker)
                .FirstOrDefaultAsync(m => m.PresentationId == id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // GET: Presentations/Create
        public IActionResult Create()
        {
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "SpeakerId", "Bio");
            return View();
        }

        // POST: Presentations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresentationId,Topic,Duration,Slides,SpeakerId")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presentation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "SpeakerId", "Bio", presentation.SpeakerId);
            return View(presentation);
        }

        // GET: Presentations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentation.FindAsync(id);
            if (presentation == null)
            {
                return NotFound();
            }
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "SpeakerId", "Bio", presentation.SpeakerId);
            return View(presentation);
        }

        // POST: Presentations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresentationId,Topic,Duration,Slides,SpeakerId")] Presentation presentation)
        {
            if (id != presentation.PresentationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentationExists(presentation.PresentationId))
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
            ViewData["SpeakerId"] = new SelectList(_context.Set<Speaker>(), "SpeakerId", "Bio", presentation.SpeakerId);
            return View(presentation);
        }

        // GET: Presentations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _context.Presentation
                .Include(p => p.Speaker)
                .FirstOrDefaultAsync(m => m.PresentationId == id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentation = await _context.Presentation.FindAsync(id);
            if (presentation != null)
            {
                _context.Presentation.Remove(presentation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresentationExists(int id)
        {
            return _context.Presentation.Any(e => e.PresentationId == id);
        }
    }
}
