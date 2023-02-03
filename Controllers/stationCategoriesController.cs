using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using puma.Models;

namespace puma.Controllers
{
    public class Data
    {
        public int catid { get; set; }
        public int stid { get; set; }
        public int stcatid { get; set; }
        public List<int> stcatidlist { get; set; }
        public string ?stname { get; set; }
        public string ?catname { get; set; }
    
    }

    public class Category
    {
        public int ID { get; set; }
        public List<int> ids { get; set; }
        public string ?Name { get; set; }
    }
    public class Stationdetails 
    { 
        public int ID { get; set; }
        public string ?Name { get; set; }
    }

    public class Lists
    {
        public List<Stationdetails> ?stations { get; set; }
        public List<Category> ?categories { get; set; }

    }
    public class Editing
    {
        public int stcatid { get; set; }
        public int stationid { get; set; }
        public int categoryid { get; set; }
        public string catname { get; set; }
        public string stationname { get; set; }

    }
    public class detailsdata {
    public List<string> detailscat { get; set; }
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
            var testdata = (from table1 in _context.StationMasters
                            join table2 in _context.stationCategories on table1.StationId equals table2.StationId
                            join table3 in _context.categoryMaster on table2.categoryId equals table3.categoryId
                            select new Data
                            {
                                catid = table3.categoryId,
                                stid = table1.StationId,
                                catname = table3.categoryName,
                                stname = table1.Name,
                                stcatid = table2.stationCategoryId
                            }).ToListAsync();



            return _context.stationCategories != null ?
                        View(await testdata) :
                        Problem("Entity set 'dbContext.stationCategories'  is null.");
        }
   
        // GET: stationCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var query = _context.StationMasters.Where(x => x.StationId == id).ToList();
            var mappedcat = (from a in _context.stationCategories where a.StationId==id select a.categoryId).ToList();
           ViewBag.categoryNames = (from b in _context.categoryMaster where mappedcat.Contains(b.categoryId) select b.categoryName).ToList();

            //if (id == null || _context.stationCategories == null)
            //{
            //    return NotFound();
            //}

            //var stationCategory = await _context.stationCategories
            //    .FirstOrDefaultAsync(m => m.stationCategoryId == id);
            //if (stationCategory == null)
            //{
            //    return NotFound();
            //}

            return View(query);
        }

        // GET: stationCategories/Create
        public IActionResult Create()
        { 
           
            var list = _context.StationMasters.ToList();
          List<int> stationids=list.Select(m=>m.StationId).ToList();
            var stationList = (from s in _context.StationMasters
                              where stationids.Contains(s.StationId)
                              select new Stationdetails {
                                  ID=s.StationId,
                                  Name=s.Name
                              }).ToList();



            var clist = _context.categoryMaster.ToList();
            List<int> catid= clist.Select(m=>m.categoryId).ToList();
            var catlist = (from c in _context.categoryMaster
                           where catid.Contains(c.categoryId)
                           select new Category
                           {
                               ID = c.categoryId,
                               Name = c.categoryName
                           }).ToList();
           
            Lists obj = new Lists() {

                categories = catlist,
                stations= stationList

            };

            SelectList stationlist = new SelectList(obj.stations.ToList());
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
          
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stationCategory);
        }



        // GET: stationCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var idd = (from a in _context.stationCategories where a.StationId == id select a).ToList();
           var list = _context.StationMasters.ToList();
          List<int> stationids=list.Select(m=>m.StationId).ToList();
            var stationList = (from s in _context.StationMasters
                              where stationids.Contains(s.StationId)
                              select new Stationdetails {
                                  ID=id,
                                  Name=s.Name
                              }).ToList();



            var clist = _context.categoryMaster.ToList();
            List<int> catid= clist.Select(m=>m.categoryId).ToList();
            var catlist = (from c in _context.categoryMaster
                           where catid.Contains(c.categoryId)
                           select new Category
                           {
                              ID=c.categoryId,
                               Name = c.categoryName
                           }).ToList();
          
            // .Where(s => catid.Contains(s.categoryId))
            // .Select(s => s.categoryName)
            //  .ToList();
            Lists obj = new Lists() {

                categories = catlist,
                stations= stationList

            };

            SelectList stationlist = new SelectList(obj.stations.ToList());
            List<Category> categorylist = obj.categories.ToList();
            ViewBag.stationlist = stationlist;
            ViewBag.categories = categorylist;

            ViewBag.stationid = id;

            return View(obj);
        }

        // POST: stationCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IEnumerable<int> catids, [Bind("stationCategoryId,StationId,categoryId")] stationCategory stationCategory)
        {
            var recordsToDelete = _context.stationCategories.Where(x => x.StationId == stationCategory.StationId);
            if (recordsToDelete.Any())
            {
                _context.stationCategories.RemoveRange(recordsToDelete);
                _context.SaveChanges();
            }
            var stid=(from a in _context.stationCategories where a.StationId == stationCategory.StationId select a.stationCategoryId).ToList();
        
            if (ModelState.IsValid)
            {
                foreach (var item in catids)
                {
                    var newStationCategory = new stationCategory
                    {
                        stationCategoryId = stid.FirstOrDefault(),
                        StationId = stationCategory.StationId,
                        categoryId = item                       
                    };
                    _context.Update(newStationCategory);
                  

                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }



        // GET: stationCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || _context.stationCategories == null)
            {
                return NotFound();
            }

            var stationCategory = await _context.stationCategories
                .FirstOrDefaultAsync(m => m.StationId == id);
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
            //var abc = _context.stationCategories.GroupBy(id => _context.stationCategories.Select(x => x.StationId));
            var recordsToDelete = _context.stationCategories.Where(x => x.StationId == id);
            if (recordsToDelete.Any())
            {
                _context.stationCategories.RemoveRange(recordsToDelete);
                await _context.SaveChangesAsync();
            }

     
            return RedirectToAction(nameof(Index));
        }

        private bool stationCategoryExists(int id)
        {
            return (_context.stationCategories?.Any(e => e.stationCategoryId == id)).GetValueOrDefault();
        }

 }

}
