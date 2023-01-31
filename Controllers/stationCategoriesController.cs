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
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class stationdetails 
    { 
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class lists
    {
        public List<stationdetails> stationnames { get; set; }
        public List<Category> categories { get; set; }

    }

    public class stationCategoriesController : Controller
    {
        private readonly dbContext _context;

        public stationCategoriesController(dbContext context)
        {
            _context = context;
        }
      
        // GET: stationCategories
        public async Task<IActionResult> Index()
        {
            return _context.stationCategories != null ?
                        View(await _context.stationCategories.ToListAsync()) :
                        Problem("Entity set 'dbContext.stationCategories'  is null.");
        }

        // GET: stationCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.stationCategories == null)
            {
                return NotFound();
            }

            var stationCategory = await _context.stationCategories
                .FirstOrDefaultAsync(m => m.stationCategoryId == id);
            if (stationCategory == null)
            {
                return NotFound();
            }

            return View(stationCategory);
        }

        // GET: stationCategories/Create
        public IActionResult Create()
        { 
           
            var list = _context.StationMasters.ToList();
          List<int> stationids=list.Select(m=>m.StationId).ToList();
            var stationList = (from s in _context.StationMasters
                              where stationids.Contains(s.StationId)
                              select new stationdetails {
                                  ID=s.StationId,
                                  Name=s.Name
                              }).ToList();


            //    stationnames
            //.Where(s => stationids.Contains(s.StationId))
            //.Select(s => s.Name)
            //.ToList();

            var clist = _context.categoryMaster.ToList();
            List<int> catid= clist.Select(m=>m.categoryId).ToList();
            var catlist = (from c in _context.categoryMaster
                           where catid.Contains(c.categoryId)
                           select new Category
                           {
                               ID = c.categoryId,
                               Name = c.categoryName
                           }).ToList();
            // .Where(s => catid.Contains(s.categoryId))
            // .Select(s => s.categoryName)
            //  .ToList();
            lists obj = new lists() {

                categories = catlist,
                stationnames= stationList

            };

            SelectList stationlist = new SelectList(obj.stationnames.ToList());
            List<Category> categorylist = obj.categories.ToList();
            ViewBag.stationlist = stationlist;
            ViewBag.categories = categorylist;  
            return View(obj);
        }

        // POST: stationCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId, categoryId")] stationCategory stationCategory, IEnumerable<int> catids)
        {
            //var query = from a in _context.stationCategories where a.stationCategoryId == _context select a;
          
          
            if (ModelState.IsValid)
            {
                foreach (var item in catids)
                {
                    var newStationCategory = new stationCategory
                    {
                        StationId = stationCategory.StationId,
                        categoryId = item
                    };
                    _context.Add(newStationCategory);
                    //stationCategory.categoryId = item;
                    //_context.Add(stationCategory);
                  
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stationCategory);
        }

        // GET: stationCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.stationCategories == null)
            {
                return NotFound();
            }

            var stationCategory = await _context.stationCategories.FindAsync(id);
            if (stationCategory == null)
            {
                return NotFound();
            }
            return View(stationCategory);
        }

        // POST: stationCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("stationCategoryId,StationId,categoryId")] stationCategory stationCategory)
        {
          
            if (id != stationCategory.stationCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stationCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!stationCategoryExists(stationCategory.stationCategoryId))
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
            return View(stationCategory);
        }

        // GET: stationCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.stationCategories == null)
            {
                return NotFound();
            }

            var stationCategory = await _context.stationCategories
                .FirstOrDefaultAsync(m => m.stationCategoryId == id);
            if (stationCategory == null)
            {
                return NotFound();
            }

            return View(stationCategory);
        }

        // POST: stationCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.stationCategories == null)
            {
                return Problem("Entity set 'dbContext.stationCategories'  is null.");
            }
            var stationCategory = await _context.stationCategories.FindAsync(id);
            if (stationCategory != null)
            {
                _context.stationCategories.Remove(stationCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool stationCategoryExists(int id)
        {
            return (_context.stationCategories?.Any(e => e.stationCategoryId == id)).GetValueOrDefault();
        }

 }

}
