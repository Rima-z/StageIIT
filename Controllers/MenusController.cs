using iit.Models;
using Microsoft.AspNetCore.Mvc;
using iit.Data;
using iit.Models;

namespace iit.Controllers
{
    public class MenusController : Controller
    {
        /// <summary>
        /// Application DB Context Object
        /// </summary>
        /// <returns></returns>

        private readonly SopalContext _db;

        /// <summary>
        /// Constructor to implement ApplicationDbContext
        /// </summary>
        /// <returns></returns>

        public MenusController(SopalContext db)
        {
            _db = db; //Getting the ApplicationDbContext that has already been registered in application
        }


        public IActionResult Index()
        {
            //Access the categories table here via ApplicationDbContext object and categories DbSet
            IEnumerable<Menus> objMenusList = _db.Menus; // Categories is the DbSet defined in ApplicationDbContext

            return View(objMenusList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        // Called when we post the create category form
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult Create(Menus obj)
        {



            //Validate the object received
            if (ModelState.IsValid)
            {

                //Add the category object to database
                _db.Menus.Add(obj);
                _db.SaveChanges(); // Saved to database

                //Using TempData for alerts
                TempData["success"] = "Category Created Successfully!";
                //After saving data redirect to index action of category
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        //Get
        public IActionResult Edit(int? CodeMenu)
        {
            if (CodeMenu == null || CodeMenu == 0)
            {
                return NotFound();
            }

            //Find the category in table with the specified id
            //var categoryFromDB = _db.Categories.SingleOrDefault(c => c.Id == id);
            var menusFromDB = _db.Menus.Find(CodeMenu);
            if (menusFromDB == null)
            {
                return NotFound();
            }

            return View(menusFromDB);
        }

        //Post
        // Called when we post the edit category form
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult Edit(Menus obj)
        {


            //Validate the object received
            if (ModelState.IsValid)
            {

                //Update the category object 
                _db.Menus.Update(obj);
                _db.SaveChanges(); // Saved to database
                                   //Using TempData for alerts
                TempData["success"] = "Category Edited Successfully!";
                //After saving data redirect to index action of category
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }



        //Get
        public IActionResult Delete(int? CodeMenu)
        {
            if (CodeMenu == null || CodeMenu == 0)
            {
                return NotFound();
            }

            //Find the category in table with the specified id
            //var categoryFromDB = _db.Categories.SingleOrDefault(c => c.Id == id);
            var menusFromDB = _db.Menus.Find(CodeMenu);
            if (menusFromDB == null)
            {
                return NotFound();
            }

            return View(menusFromDB);
        }

        //Post

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // Helps in preventing cross site request forgery attacks
        public IActionResult DeletePOST(int? CodeMenu)
        {
            var obj = _db.Menus.Find(CodeMenu);
            if (obj == null)
            {
                return NotFound();
            }


            //Update the category object 
            _db.Menus.Remove(obj);
            _db.SaveChanges(); // Saved to database
                               //Using TempData for alerts
            TempData["success"] = "Category Deleted Successfully!";

            //After saving data redirect to index action of category
            return RedirectToAction("Index");

        }

    }
}
