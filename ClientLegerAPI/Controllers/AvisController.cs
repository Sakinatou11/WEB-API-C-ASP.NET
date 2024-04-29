using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AvisController : Controller
    {
        private readonly WebAPIContext _context;

        public AvisController(WebAPIContext context)
        {
            _context = context;
        }

        // GET: Avis
        public async Task<IActionResult> Index()
        {
            var avis = await _context.Avis.ToListAsync();
            return View(avis);
        }

        // GET: Avis/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FirstOrDefaultAsync(m => m.Id == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // GET: Avis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,ProductName")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avis);
        }

        // GET: Avis/Edit/{id}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FindAsync(id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // POST: Avis/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,ProductName")] Avis avis)
        {
            if (id != avis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvisExists(avis.Id))
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
            return View(avis);
        }

        // GET: Avis/Delete/{id}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var avis = await _context.Avis.FirstOrDefaultAsync(m => m.Id == id);
            if (avis == null)
            {
                return NotFound();
            }

            return View(avis);
        }

        // POST: Avis/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var avis = await _context.Avis.FindAsync(id);
            _context.Avis.Remove(avis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvisExists(Guid id)
        {
            return _context.Avis.Any(e => e.Id == id);
        }
    }
}
