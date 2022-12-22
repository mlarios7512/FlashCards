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
using FlashCards.Models.ViewModel;
using System.Runtime.Intrinsics.X86;
using System;
using FlashCards.DAL.Abstract;

namespace FlashCards.Controllers
{
    public class PersonalController : Controller
    {
        private readonly ILogger<PersonalController> _logger;
        private FlashCardsDBContext db;
        private ICardSetRepository _cardSetRepository;

        public PersonalController(ILogger<PersonalController> logger, FlashCardsDBContext db, ICardSetRepository cardRepo)
        {
            _logger = logger;
            this.db = db;
            _cardSetRepository = cardRepo;
        }

        [HttpGet]
        public IActionResult TestCardEdits(int cardSetId)
        {
            CardSet SetToEdit = db.CardSets.Include(s => s.Cards).Single(set => set.Id == cardSetId);

            EditSetVM EditSetVM = new EditSetVM(SetToEdit.Cards.Count);
            EditSetVM.SetId = SetToEdit.Id;
            EditSetVM.SetName = SetToEdit.Name;
            EditSetVM.MaxCardsPerSet = _cardSetRepository.GetMaxCardsAllowed();

            for (int i = 0; i < SetToEdit.Cards.Count; i++)
            {
                Debug.WriteLine("ID of card: " + SetToEdit.Cards.ElementAt(i).Id);
                EditSetVM.CardIDsInSet[i] = SetToEdit.Cards.ElementAt(i).Id;
                EditSetVM.FrontCard[i] = SetToEdit.Cards.ElementAt(i).FrontCard;
                EditSetVM.BackCard[i] = SetToEdit.Cards.ElementAt(i).BackCard;
            }
            EditSetVM.CardsToDelete = new bool[SetToEdit.Cards.Count];
            

            return View(EditSetVM);
        }

        //ERROR: Retrieves the wrong cards??? (needs further look into.)
        //(The view only has 2 cards (even thought the DB is NOT being modified.)
        [HttpPost]
        public IActionResult TestCardEdits(EditSetVM editedSet) 
        {
            for (int i = 0; i < editedSet.FrontCard.Length; i++) 
            {
                Card UpdatedCard = db.Cards.SingleOrDefault(c => c.Id == editedSet.CardIDsInSet[i]);

                db.Cards.Update(UpdatedCard);
                db.SaveChanges();
            }

            //for (int i = 0; i < editedSet.CardsToDelete.Length; i++) 
            //{
            //    if (editedSet.CardsToDelete[i] == true)
            //    {
            //        Card CardToRemove = db.Cards.Single(c => c.Id == editedSet.CardIDsInSet[i]);
            //        Debug.WriteLine("Card removed: " + '\n' +
            //            "Front: " + CardToRemove.FrontCard + '\n' +
            //            "Back: " + CardToRemove.BackCard);

            //        //db.Cards.Remove(CardToRemove);
            //        //db.SaveChanges();
            //    }
            //}

            return View(editedSet);
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
        public IActionResult CreateSet(int curUserId, int newCardAmount)
        {
            ViewBag.NoError = true;

            CreateNewCardSetVM NewCardSet = new CreateNewCardSetVM(newCardAmount);
            NewCardSet.CurUserId = curUserId;
            return View(NewCardSet);
        }

        [HttpPost]
        public IActionResult CreateSet(CreateNewCardSetVM newSetInput)
        {

            if (ModelState.IsValid == true)
            {
                CardSet NewCardSet = new CardSet();
                NewCardSet.Name = newSetInput.SetName;
                NewCardSet.UserOwnerId = newSetInput.CurUserId;

                db.CardSets.Add(NewCardSet);
                db.SaveChanges();


                Card NewCard;
                int NewSetId = db.CardSets.Single(set => set.Name == newSetInput.SetName).Id;
                for (int i = 0; i < newSetInput.CardBackSide.Length; i++) 
                {
                    NewCard = new FlashCards.Models.Card();
                    NewCard.CardSetId = NewSetId;
                    NewCard.FrontCard = newSetInput.CardFrontSide[i];
                    NewCard.BackCard = newSetInput.CardBackSide[i];

                    db.Cards.Add(NewCard);
                    db.SaveChanges();
                }
                


                return RedirectToAction("ViewSets", "Personal", new { curUserId = newSetInput.CurUserId });
            }
            return RedirectToAction("CreateSet", new { curUserId = (int)newSetInput.CurUserId, newCardAmount = (int)newSetInput.CardFrontSide.Length } );
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
