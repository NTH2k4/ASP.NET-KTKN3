using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _57361.Data;
using _57361.Models;

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
        public async Task<IActionResult> Index()
        {
            var _57361Context = _context.Presentation.Include(p => p.Speaker);
            return View(await _57361Context.ToListAsync());
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
