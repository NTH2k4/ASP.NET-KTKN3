using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NguyenTrungHieu.Data;
using NguyenTrungHieu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace NguyenTrungHieu.Controllers
{
    public class PerformancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PerformancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index(string? searchString, string? sortOrder, int page = 1, int pageSize = 3)
        {
            var performances = _context.Performance.Select(p => new PerformanceViewModel
            {
                PerformanceId = p.TheaterId,
                Title = p.Title,
                Date = p.Date,
                TicketPrice = p.TicketPrice
            });
            if (!searchString.IsNullOrEmpty())
            {
                performances = performances.Where(p => p.Title.ToLower().Contains(searchString.ToLower()));
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSort"] = sortOrder.IsNullOrEmpty() ? "title_desc" : "";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
            performances = sortOrder switch
            {
                "title_desc" => performances.OrderByDescending(p => p.Title),
                "date" => performances.OrderBy(p => p.Date),
                "date_desc" => performances.OrderByDescending(p => p.Date),
                "price" => performances.OrderBy(p => p.TicketPrice),
                "price_desc" => performances.OrderByDescending(p => p.TicketPrice),
                _ => performances.OrderBy(p => p.Title)
            };
            return View(performances.ToPagedList(page, pageSize));
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.theater)
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // GET: Performances/Create
        public IActionResult Create()
        {
            ViewData["TheaterId"] = new SelectList(_context.Theater, "TheaterId", "Name");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerformanceId,Title,Date,TicketPrice,TheaterId")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TheaterId"] = new SelectList(_context.Theater, "TheaterId", "Name", performance.TheaterId);
            return View(performance);
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            ViewData["TheaterId"] = new SelectList(_context.Theater, "TheaterId", "Name", performance.TheaterId);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerformanceId,Title,Date,TicketPrice,TheaterId")] Performance performance)
        {
            if (id != performance.PerformanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.PerformanceId))
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
            ViewData["TheaterId"] = new SelectList(_context.Theater, "TheaterId", "Name", performance.TheaterId);
            return View(performance);
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performance
                .Include(p => p.theater)
                .FirstOrDefaultAsync(m => m.PerformanceId == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performance.FindAsync(id);
            if (performance != null)
            {
                _context.Performance.Remove(performance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performance.Any(e => e.PerformanceId == id);
        }
    }
}
