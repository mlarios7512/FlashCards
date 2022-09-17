using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlashCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FlashCardsDBContext db;

        public HomeController(ILogger<HomeController> logger, FlashCardsDBContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            ViewBag.NoError = true;
            ViewBag.UserNameTaken = false;

            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(User newUser)
        {
            if (ModelState.IsValid)
            {
                User ExistingUser = db.Users.SingleOrDefault(existingUser => existingUser.Username == newUser.Username);
                if (ExistingUser == null) 
                {
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    return RedirectToAction("Personal", "ViewSets", new { curUserId = newUser.Id });
                }
                else 
                {
                    ViewBag.UserNameTaken = true;
                }
            }
            else 
            {
                ViewBag.NoError = false;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.NoError = true;
            return View();
        }

        ////NOT WORKING---DEBUG
        [HttpPost]
        public IActionResult Login(User returningUser)
        {
            ViewBag.NoError = false;

            User PotentialExistingUser = db.Users.SingleOrDefault(user => (user.Username == returningUser.Username)
                && (user.Password == returningUser.Password));

            if (PotentialExistingUser != null)
            {
                return RedirectToAction("ViewSets", "Personal", new { curUserId = PotentialExistingUser.Id });
            }


            return View();
        }



        //[HttpGet]
        //public IActionResult AddCard()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddCard(Card newCard)
        //{
        //    return View();
        //}

        //User FirstUser = new User();
        //CardSet SetOne = new CardSet("Gaming Trivia");
        //Card CardOne = new Card("Most succesful gaming console in the 90s?", "Sony Playstation 1");
        //Card CardTwo = new Card("Most popular game on NES?", "Super mario bros 2");




        //[HttpGet]
        //public IActionResult ViewSets(User user)
        //{
        //    FirstUser.MySets.Add(SetOne);
        //    return View(FirstUser);
        //}

        //[HttpGet]
        //public IActionResult ReviewSet() 
        //{
        //    FirstUser.MySets.Add(SetOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

        //    CardSet setToView = FirstUser.MySets.First();

        //    return View(setToView);
        //}

        //public IActionResult EditSet() 
        //{
        //    FirstUser.MySets.Add(SetOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

        //    CardSet setToView = FirstUser.MySets.First();

        //    return View(setToView);
        //}
        //[HttpGet]
        //public IActionResult CreateSet() 
        //{
        //    FirstUser.MySets.Add(SetOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
        //    FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

        //    CardSet setToView = FirstUser.MySets.First();
        //    return View(setToView);
        //}

        //[HttpPost]
        //public IActionResult CreateSet(CardSet cardset)
        //{
        //    if(cardset.Name != null)
        //    {
        //        // cardset.UserOwnerId = 
        //        // context.CardSets.Add(cardset);
        //        // return RedirectToAction("Home", "ViewSets", new {curUserId = returningUser.Id});
        //    }

        //    //Do not redirect to "ViewSets" page.
        //    else
        //    {

        //    }

        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}