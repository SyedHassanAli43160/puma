using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using puma.Models;

namespace puma.Controllers
{
   
    public class categoryMastersController : Controller
    {
        private readonly dbContext _context;

        public categoryMastersController(dbContext context)
        {
            _context = context;
        }

        // GET: categoryMasters
        public async Task<IActionResult> Index()
        {
              return _context.categoryMaster != null ? 
                          View(await _context.categoryMaster.ToListAsync()) :
                          Problem("Entity set 'dbContext.categoryMaster'  is null.");
        }

        // GET: categoryMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.categoryMaster == null)
            {
                return NotFound();
            }

            var categoryMaster = await _context.categoryMaster
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (categoryMaster == null)
            {
                return NotFound();
            }

            return View(categoryMaster);
        }

        // GET: categoryMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: categoryMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,categoryName,isdisabled")] categoryMaster categoryMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryMaster);
        }

        // GET: categoryMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.categoryMaster == null)
            {
                return NotFound();
            }

            var categoryMaster = await _context.categoryMaster.FindAsync(id);
            if (categoryMaster == null)
            {
                return NotFound();
            }
            return View(categoryMaster);
        }

        // POST: categoryMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryId,categoryName,isdisabled")] categoryMaster categoryMaster)
        {
            if (id != categoryMaster.categoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!categoryMasterExists(categoryMaster.categoryId))
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
            return View(categoryMaster);
        }

        // GET: categoryMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.categoryMaster == null)
            {
                return NotFound();
            }

            var categoryMaster = await _context.categoryMaster
                .FirstOrDefaultAsync(m => m.categoryId == id);
            if (categoryMaster == null)
            {
                return NotFound();
            }

            return View(categoryMaster);
        }

        // POST: categoryMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.categoryMaster == null)
            {
                return Problem("Entity set 'dbContext.categoryMaster'  is null.");
            }
            var categoryMaster = await _context.categoryMaster.FindAsync(id);
            if (categoryMaster != null)
            {
                _context.categoryMaster.Remove(categoryMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool categoryMasterExists(int id)
        {
          return (_context.categoryMaster?.Any(e => e.categoryId == id)).GetValueOrDefault();
        }
    }
}
