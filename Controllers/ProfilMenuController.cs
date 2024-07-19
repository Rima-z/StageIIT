using iit.Data;
using iit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iit.Controllers
{
    public class ProfilMenuController : Controller
    {
        private readonly SopalContext _db;

        public ProfilMenuController(SopalContext db)
        {
            _db = db;
        }
        // GET: ProfilMenu/Index
        public IActionResult Index()
        {
            IEnumerable<ProfilMenu> objProfilMenuList = _db.ProfilMenu.ToList();
            return View(objProfilMenuList);
        }

        // GET: ProfilMenu/Delete/5
        public IActionResult Delete(string id)
        {
            var fonction = _db.ProfilMenu.Find(id);
            if (fonction != null)
            {
                _db.ProfilMenu.Remove(fonction);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        // Afficher le formulaire de création
        public IActionResult Create()
        {
            return View();
        }

        // Enregistrer un nouvel ProfilMenu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfilMenu obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProfilMenu.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "ProfilMenu créé avec succès !";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // Méthode pour obtenir les menus d'un ProfilMenu
        public IActionResult GetMenus(string codPrm)
        {
            var profilMenu = _db.ProfilMenu.FirstOrDefault(pm => pm.CodPrm == codPrm);
            if (profilMenu == null)
            {
                return NotFound();
            }

            var menuIds = profilMenu.Menu.Split(',').Select(int.Parse).ToList();
            var menus = _db.Menus.Where(m => menuIds.Contains(m.CodeMenu)).ToList();

            return PartialView("_MenuListPartial", menus);
        }
    }
}