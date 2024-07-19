using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iit.Data;
using iit.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace iit.Controllers
{
    public class ProfilFonctionController : Controller
    {
        private readonly SopalContext _db;

        public ProfilFonctionController(SopalContext db)
        {
            _db = db;
        }

        public IActionResult GetFonctions(string codPrf)
        {
            var fonctions = _db.Droits
                               .Where(d => d.CodPrf == codPrf)
                               .Include(d => d.Fonction)
                               .Select(d => d.Fonction)
                               .ToList();
            return Json(fonctions);
        }

        public IActionResult Index()
        {
            IEnumerable<ProfilFonction> objProfilFonctionList = _db.ProfilFonction.ToList();
            return View(objProfilFonctionList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProfilFonction obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProfilFonction.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "ProfilFonction créé avec succès !";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(string codPrf)
        {
            if (string.IsNullOrEmpty(codPrf))
            {
                return NotFound();
            }

            var profilFonctionFromDB = _db.ProfilFonction.Find(codPrf);
            if (profilFonctionFromDB == null)
            {
                return NotFound();
            }

            return View(profilFonctionFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfilFonction obj)
        {
            if (ModelState.IsValid)
            {
                _db.ProfilFonction.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "ProfilFonction modifié avec succès !";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(string id)
        {
            var profilFonction = _db.ProfilFonction.Find(id);
            if (profilFonction != null)
            {
                _db.ProfilFonction.Remove(profilFonction);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
