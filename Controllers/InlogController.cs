using BusnLogicLaag;
using DALMSSQLSERVER;
using Microsoft.AspNetCore.Mvc;
using OutfitKing.Models;

namespace OutfitKing.Controllers
{
    /// <summary>
    /// 1. Request (button click) gaat naar controller toe 
    /// 2. Controller gebruikt een model om data uit te halen voor die action
    /// 3. De data wordt gebruikt in de view
    /// 4. De view geeft info terug aan de controller
    /// 5. De controller geeft een antwoord op de request (pagina wordt getoond)
    /// </summary>
    public class InlogController : Controller
    {
        /// <summary>
        /// url = https/:localhost/{controller}/{action}/{id} ('pattern' in program.cs)
        /// </summary>

        private readonly ILogger<InlogController> _logger;
        //wat doet een logger?
        //want je zou in de ctor ook als parameter een database kunnen meegeven , dus wat is de logica
        public GebruikerContainer gebrContainer = new GebruikerContainer(new GebruikerMSSQLDAL());

        public InlogController(ILogger<InlogController> logger)
        {
            _logger = logger;
        }
        
        public ActionResult Index()
        //Verschil IactionResult en ActionResult ??
        {
            GebruikerVM gebrVM = new GebruikerVM();
            gebrVM.Retry = false;
            return View(gebrVM);
        }

        [HttpPost]
        public IActionResult Index(GebruikerVM gebruiker)
        {
            /// <summary>
            /// iActionresult kan meerdere returns handelen
            /// </summary>

            /// <summary>
            /// als er een gebr wordt gevonden (!= null), activeer dan de session.
            /// vervolgens ga terug naar homepagina (index)
            /// </summary>
            /// 
            Gebruiker gebr = gebrContainer.ZoekGebrOpGebrnaamEnWW(gebruiker.Gerbuikersnaam, gebruiker.Wachtwoord);
            if (gebr != null)
            {
                // waarom geef je de session gebruiker mee en niet gebruikersnaam
                HttpContext.Session.SetString("Gebruiker", gebr.Gerbuikersnaam);
                return RedirectToAction("Index", "Home", gebruiker);
            }
            else
            {
                GebruikerVM gebrVM = new GebruikerVM();
                gebrVM.Retry = true;
                return View(gebrVM);
            }
        }
        
        [HttpPost]
        public IActionResult LogOut(GebruikerVM gebruiker)
        {
            Gebruiker gebr = gebrContainer.ZoekGebrOpGebrnaamEnWW(gebruiker.Gerbuikersnaam, gebruiker.Wachtwoord);
            if (gebr != null)
            {
                // Doet mijn session het? en waar staat de eerste " " voor in de session?
                HttpContext.Session.SetInt32("ID", gebr.ID);
                return RedirectToAction("Index", "Home", gebruiker);
            }
            else
            {
                GebruikerVM gebrVM = new GebruikerVM();
                gebrVM.Retry = true;
                return View(gebrVM);
            }
        }
    }
}
