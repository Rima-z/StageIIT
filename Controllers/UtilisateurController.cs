using System.Linq;
using Microsoft.AspNetCore.Mvc;
using iit.Data;
using iit.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace iit.Controllers
{
    public class UtilisateurController : Controller
    {
        private readonly SopalContext _db;

        public UtilisateurController(SopalContext db)
        {
            _db = db;
        }




        // Afficher la liste des utilisateurs
        public IActionResult Index()
        {
            IEnumerable<Utilisateur> objUtilisateurList = _db.Utilisateurs.ToList();
            return View(objUtilisateurList);
        }

        // Afficher le formulaire de création
        public IActionResult Create()
        {
            return View();
        }

        
        // Enregistrer un nouvel utilisateur depuis le formulaire de création
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Ajouter le préfixe "SOPAL\" au nom d'utilisateur
                    utilisateur.Username = "SOPAL\\" + utilisateur.Username;

                    _db.Utilisateurs.Add(utilisateur);
                    await _db.SaveChangesAsync();
                    return Ok();
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (uncomment ex variable name and write a log.)
                    return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
                }
            }

            return BadRequest(ModelState);
        }






        // Afficher le formulaire d'inscription
        public IActionResult Register()
        {
            return View();
        }

        // Enregistrer un nouvel utilisateur via le formulaire d'inscription
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Utilisateur obj)
        {
            if (ModelState.IsValid)
            {
                // Ajouter le préfixe "SOPAL\" au nom d'utilisateur
                obj.Username = "SOPAL\\" + obj.Username;

                _db.Utilisateurs.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Inscription réussie !";
                return RedirectToAction("Login"); // Rediriger vers la page de connexion
            }
            return View(obj);
        }

        // Afficher le formulaire de connexion
        public IActionResult Login()
        {
            return View();
        }

        // Action pour traiter le formulaire de connexion
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = _db.Utilisateurs.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (utilisateur != null)
                {
                    // Create the identity from the user info
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilisateur.Username)
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Redirect to home
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
            }
            return View(model);
        }


       

        // GET: Afficher le formulaire d'édition
        public IActionResult Edit(int? CodUsr)
        {
            if (CodUsr == null || CodUsr == 0)
            {
                return NotFound();
            }

            var utilisateurFromDB = _db.Utilisateurs.Find(CodUsr);
            if (utilisateurFromDB == null)
            {
                return NotFound();
            }

            return View(utilisateurFromDB);
        }

        // POST: Enregistrer les modifications après édition
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Utilisateur obj)
        {
            if (ModelState.IsValid)
            {
                _db.Utilisateurs.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Utilisateur modifié avec succès !";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: Afficher la vue de confirmation de suppression
        public IActionResult ConfirmDelete(int? CodUsr)
        {
            if (CodUsr == null || CodUsr == 0)
            {
                return NotFound();
            }

            var utilisateurFromDB = _db.Utilisateurs.Find(CodUsr);
            if (utilisateurFromDB == null)
            {
                return NotFound();
            }

            return View(utilisateurFromDB);
        }



        public IActionResult Delete(int id)
        {
            var utilisateur = _db.Utilisateurs.Find(id);
            if (utilisateur != null)
            {
                _db.Utilisateurs.Remove(utilisateur);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
