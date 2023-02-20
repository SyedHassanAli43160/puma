using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using puma.Models;

namespace puma.Controllers
{
    public class mapclass
    {
        public double lat { get; set; }

        public double lng { get; set; }

        public string title { get; set; }
    }
    public class stationMastersController : Controller
    {
        private readonly dbContext _context;

        public stationMastersController(dbContext context)
        {
            _context = context;
        }

        // GET: stationMasters
        public async Task<IActionResult> Index()
        {
              return _context.StationMasters != null ? 
                          View(await _context.StationMasters.ToListAsync()) :
                          Problem("Entity set 'dbContext.stationMaster'  is null.");
        }

        // GET: stationMasters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StationMasters == null)
            {
                return NotFound();
            }

            var stationMaster = await _context.StationMasters
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (stationMaster == null)
            {
                return NotFound();
            }

            return View(stationMaster);
        }

        // GET: stationMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: stationMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StationId,Name,area,Description,city,latitude,longtitude")] stationMaster stationMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stationMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stationMaster);
        }

        // GET: stationMasters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StationMasters == null)
            {
                return NotFound();
            }

            var stationMaster = await _context.StationMasters.FindAsync(id);
            if (stationMaster == null)
            {
                return NotFound();
            }
            return View(stationMaster);
        }

        // POST: stationMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationId,Name,area,Description,city,latitude,longtitude")] stationMaster stationMaster)
        {
            if (id != stationMaster.StationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stationMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!stationMasterExists(stationMaster.StationId))
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
            return View(stationMaster);
        }

        // GET: stationMasters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StationMasters == null)
            {
                return NotFound();
            }

            var stationMaster = await _context.StationMasters
                .FirstOrDefaultAsync(m => m.StationId == id);
            if (stationMaster == null)
            {
                return NotFound();
            }

            return View(stationMaster);
        }

        // POST: stationMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StationMasters == null)
            {
                return Problem("Entity set 'dbContext.stationMaster'  is null.");
            }
            var stationMaster = await _context.StationMasters.FindAsync(id);
            if (stationMaster != null)
            {
                _context.StationMasters.Remove(stationMaster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool stationMasterExists(int id)
        {
          return (_context.StationMasters?.Any(e => e.StationId == id)).GetValueOrDefault();
        }
        public IActionResult map()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.StationMasters = getstationlist();
            mymodel.categoryMaster = getcategories();
            //var locations = _context.StationMasters.ToList();

            return View(mymodel);
            
         
        }
        [HttpPost]
        public IActionResult map(string city,IEnumerable<string> service)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.StationMasters = getstationlist();
            mymodel.categoryMaster = getcategories();

            if (city!=null)
            {
   
                var cityname = _context.StationMasters.Where(x => x.city == city).FirstOrDefault();
         
                if (city==cityname.city)
                {
                    var ids = _context.stationCategories.ToList();
                     
                    var res = (from c in _context.StationMasters
                               join d in _context.stationCategories
                               on c.StationId equals d.StationId
                               join e in _context.categoryMaster
                               on d.categoryId equals e.categoryId
                               where c.city==city && service.Contains(e.categoryName)
                               //service.Any(x=>x== e.categoryName)

                               select new mapclass
                               {
                                   lat = c.latitude,
                                   lng = c.longtitude,
                                   title = c.Name+c.city
                               }).ToList();
                    ViewData["Locations"] = res;

                    return Json(new { locations = res });
                }
            }

            return Json(new { locations = new List<mapclass>() });
        }
        private List<stationMaster> getstationlist()
        {
            
            return _context.StationMasters.ToList();
        }
       private List<categoryMaster> getcategories()
        {
            var a = _context.categoryMaster.Where(x => x.isdisabled == false).ToList();
            return a;
        }
        private List<stationCategory> stationcategories()
        {
            return _context.stationCategories.ToList();
        }
    }
}
