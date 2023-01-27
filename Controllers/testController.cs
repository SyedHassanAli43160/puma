using Microsoft.AspNetCore.Mvc;
using puma.Models;

namespace puma.Controllers
{
  
    public class loc
    {

        public double lat { get; set; }

        public double lng { get; set; }

        public string title { get; set; }
    }
    public class testController : Controller
    {
        dbContext db;
        public testController(dbContext db) 
        {
            this.db = db;
        }
        public IActionResult Create(test user)
        {
            using (db)
            {
                if(ModelState.IsValid)
                {
                    db.tests.Add(user);
                    db.SaveChanges();

                }
               
            }
            return View();
        }
        public IActionResult loadmap()
        {
            var locations = db.tests.ToList();

            var sel = (from c in locations
                       select new loc
                       {
                           lat = c.latitude,
                           lng = c.longitude,
                           title = c.Id.ToString()
                       }).ToList();
            ViewData["Locations"] = sel;
            return View(locations);
        }
        public IActionResult data()
        {
            return View(db.tests.ToList());

        }



    }
}
