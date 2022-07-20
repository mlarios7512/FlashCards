using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlashCards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        DBContext context = new DBContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount()
        {
            ViewBag.NoError = true;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(User newUser)
        {
            if (ModelState.IsValid == true)
            {
                User PotentialUser = context.Users.SingleOrDefault(User => User.Username == newUser.Username);
                if(PotentialUser == null)
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                    return RedirectToAction("ViewSets", "Personal", new {curUserId = newUser.Id});
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

        //NOT WORKING---DEBUG
        [HttpPost]
        public IActionResult Login(User returningUser)
        {
            ViewBag.NoError = false;
            
            User PotentialExistingUser = context.Users.SingleOrDefault(user => (user.Username == returningUser.Username)
                && (user.Password == returningUser.Password));

            if(PotentialExistingUser != null)
            {
                return RedirectToAction("ViewSets", "Personal", new{curUserId = PotentialExistingUser.Id});
            }
                
            
                return View();
        }



        [HttpGet]
        public IActionResult AddCard()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCard(Card newCard)
        {
            return View();
        }

        User FirstUser = new User();
        CardSet SetOne = new CardSet("Gaming Trivia");
        Card CardOne = new Card("Most succesful gaming console in the 90s?", "Sony Playstation 1");
        Card CardTwo = new Card("Most popular game on NES?", "Super mario bros 2");




        [HttpGet]
        public IActionResult ViewSets(User user)
        {
            FirstUser.MySets.Add(SetOne);
            return View(FirstUser);
        }

        [HttpGet]
        public IActionResult ReviewSet() 
        {
            FirstUser.MySets.Add(SetOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

            CardSet setToView = FirstUser.MySets.First();

            return View(setToView);
        }

        public IActionResult EditSet() 
        {
            FirstUser.MySets.Add(SetOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

            CardSet setToView = FirstUser.MySets.First();

            return View(setToView);
        }
        [HttpGet]
        public IActionResult CreateSet() 
        {
            FirstUser.MySets.Add(SetOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardOne);
            FirstUser.MySets.ElementAt(0).Cards.Add(CardTwo);

            CardSet setToView = FirstUser.MySets.First();
            return View(setToView);
        }

        [HttpPost]
        public IActionResult CreateSet(CardSet cardset)
        {
            if(cardset.Name != null)
            {
                // cardset.UserOwnerId = 
                // context.CardSets.Add(cardset);
                // return RedirectToAction("Home", "ViewSets", new {curUserId = returningUser.Id});
            }
            
            //Do not redirect to "ViewSets" page.
            else
            {
                
            }

            return View();
        }

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