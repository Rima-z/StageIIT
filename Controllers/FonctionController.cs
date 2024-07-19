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
    public class FonctionController : Controller
    {
        private readonly SopalContext _db;

        public FonctionController(SopalContext db)
        {
            _db = db;
        }

        // GET: Fonction/Index
        public IActionResult Index()
        {
            IEnumerable<Fonction> objFonctionList = _db.Fonctions.ToList();
            return View(objFonctionList);
        }

        // GET: Fonction/Create
        public IActionResult Create()
        {
            var options = _db.Options.ToList();
            ViewBag.Options = new SelectList(options, "CodeOp", "Libelle");
            return View();
        }

        // POST: Fonction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fonction obj)
        {
            if (ModelState.IsValid)
            {
                _db.Fonctions.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Fonction créée avec succès !";
                return RedirectToAction("Index");
            }

            var options = _db.Options.ToList();
            ViewBag.Options = new SelectList(options, "CodeOp", "L  ibelle");
            return View(obj);
        }

        // GET: Fonction/Edit/5
        public IActionResult Edit(int? CodeF)
        {
            if (CodeF == null)
            {
                return NotFound();
            }

            var fonctionFromDB = _db.Fonctions.Find(CodeF);
            if (fonctionFromDB == null)
            {
                return NotFound();
            }

            var options = _db.Options.ToList();
            ViewBag.Options = new SelectList(options, "CodeOp", "Libelle");
            return View(fonctionFromDB);
        }

        // POST: Fonction/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int CodeF, Fonction obj)
        {
            if (CodeF != obj.CodeF)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Fonctions.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Fonction modifiée avec succès !";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    throw;
                }
            }

            var options = _db.Options.ToList();
            ViewBag.Options = new SelectList(options, "CodeOp", "Libelle");
            return View(obj);
        }

        // GET: Fonction/Delete/5
        public IActionResult Delete(int id)
        {
            var fonction = _db.Fonctions.Find(id);
            if (fonction != null)
            {
                _db.Fonctions.Remove(fonction);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
