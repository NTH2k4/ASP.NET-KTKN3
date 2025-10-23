using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _57362.Data;
using _57362.Models;

namespace _57362.Controllers
{
    public class CuratorsController : Controller
    {
        private readonly _57362Context _context;

        public CuratorsController(_57362Context context)
        {
            _context = context;
        }

        // GET: Curators
        public async Task<IActionResult> Index()
        {
            return View(await _context.Curator.ToListAsync());
        }

        // GET: Curators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curator = await _context.Curator
                .FirstOrDefaultAsync(m => m.CuratorId == id);
            if (curator == null)
            {
                return NotFound();
            }

            return View(curator);
        }

        // GET: Curators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Curators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuratorId,Name,Museum,Specialization")] Curator curator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(curator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curator);
        }

        // GET: Curators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curator = await _context.Curator.FindAsync(id);
            if (curator == null)
            {
                return NotFound();
            }
            return View(curator);
        }

        // POST: Curators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuratorId,Name,Museum,Specialization")] Curator curator)
        {
            if (id != curator.CuratorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuratorExists(curator.CuratorId))
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
            return View(curator);
        }

        // GET: Curators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curator = await _context.Curator
                .FirstOrDefaultAsync(m => m.CuratorId == id);
            if (curator == null)
            {
                return NotFound();
            }

            return View(curator);
        }

        // POST: Curators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var curator = await _context.Curator.FindAsync(id);
            if (curator != null)
            {
                _context.Curator.Remove(curator);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuratorExists(int id)
        {
            return _context.Curator.Any(e => e.CuratorId == id);
        }
    }
}
