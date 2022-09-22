using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace FlashCards.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private FlashCardsDBContext db;

        public PersonalController(ILogger<PersonalController> logger, FlashCardsDBContext db)
        {
            _logger = logger;
            this.db = db;
        }


        [HttpGet]
        public async Task<IActionResult> ViewSets(int curUserId)
        {
            User CurrentUser = await db.Users.SingleAsync(user => user.Id == curUserId);
            CurrentUser.CardSets = await db.CardSets.Where(cs => cs.UserOwnerId == curUserId).ToListAsync();

            for(int i = 0; i < CurrentUser.CardSets.Count; i++) 
            {
                CurrentUser.CardSets.ElementAt(i).Cards = await db.Cards.Where(card => card.CardSetId == CurrentUser.CardSets.ElementAt(i).Id).ToListAsync();
            }

            return View(CurrentUser);
        }


        [HttpGet]
        public IActionResult CreateSet(int curUserId) //REPLACE CardSet W/ a ViewModel.
        {
            ViewBag.NoError = true;

            CardSet NewCardSet = new CardSet();
            NewCardSet.UserOwnerId = curUserId;

            return View(NewCardSet);
        }

        [HttpPost]
        public IActionResult CreateSet(CardSet newCard) //REPLACE CardSet W/ a ViewModel.
        {
            ViewBag.NoError = false;

            if (newCard.Name != null)
            {
                db.CardSets.Add(newCard);
                db.SaveChanges();

                return RedirectToAction("ViewSets", "Personal", new { curUserId = newCard.UserOwnerId });
            }
            return View(newCard);
        }

        [HttpGet]
        public IActionResult EditSet(int cardSetId)
        {
            CardSet CurSet = new CardSet();
            CurSet = db.CardSets.SingleOrDefault(set => set.Id == cardSetId);
            CurSet.Cards = db.Cards.Where(card => card.CardSetId == cardSetId).ToList();

            return View(CurSet);
        }

        [HttpPost]
        public IActionResult DeleteSet(int setToDeleteId)
        {
            CardSet CardSetToRemove = new CardSet();
            CardSetToRemove = db.CardSets.Single(cs => cs.Id == setToDeleteId);

            List<Card> PotentialCardsToRemove = db.Cards.Where(c => c.CardSetId == setToDeleteId).ToList();
            //db.Cards.RemoveRange(PotentialCardsToRemove);

            foreach (Card cardToRemove in PotentialCardsToRemove) 
            {
                db.Cards.Remove(cardToRemove);
            }

            db.CardSets.Remove(CardSetToRemove);
            db.SaveChanges();

            return RedirectToAction("ViewSets", "Personal", new { curUserId = CardSetToRemove.UserOwnerId });
        }

        [HttpGet]
        public IActionResult ReviewSet(int cardSetId)
        {
            CardSet CurSet = db.CardSets.Single(set => set.Id == cardSetId);
            CurSet.Cards = db.Cards.Where(cards => cards.CardSetId == cardSetId).ToList();

            return View(CurSet);
        }


        [HttpPost]
        public IActionResult AddCardToSet(CardSet curSet, string cardDef, string cardExplanation)
        {
            ViewBag.NoError = false;
            if ((cardDef != null) && (cardExplanation != null))
            {
                Card newCard = new Card();
                newCard.FrontCard = cardDef;
                newCard.BackCard = cardExplanation;
                newCard.CardSetId = curSet.Id;

                db.Cards.Add(newCard);
                db.SaveChanges();

                return RedirectToAction("ViewSets", "Personal", new { curUserId = curSet.UserOwnerId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCard(int cardToEditID)
        {
            ViewBag.NoError = true;

            Card CurCard = new Card();
            CurCard = db.Cards.SingleOrDefault(card => card.Id == cardToEditID);

            return View(CurCard);
        }

        [HttpPost]
        public IActionResult EditCard(Card curCard)
        {
            ViewBag.NoError = false;
            if ((curCard.FrontCard != null) && (curCard.BackCard != null))
            {
                ViewBag.NoError = true;

                db.Cards.Update(curCard);
                db.SaveChanges();

                return RedirectToAction("EditSet", "Personal", new { cardSetId = curCard.CardSetId });
            }
            return View(curCard);
        }

        [HttpPost]
        public IActionResult DeleteCard(int cardToDeleteId)
        {
            if (cardToDeleteId != 0)
            {
                Card CardToRemove = new Card();
                CardToRemove = db.Cards.Single(card => card.Id == cardToDeleteId);

                db.Cards.Remove(CardToRemove);
                db.SaveChanges();

                return RedirectToAction("EditSet", "Personal", new { cardSetId = CardToRemove.CardSetId});
            }

            return View();
        }
    }
    
}
