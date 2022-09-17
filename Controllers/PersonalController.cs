using FlashCards.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        //[HttpGet]
        //public IActionResult CreateSet(int curUserId) //WORKING W/ DB
        //{
        //    ViewBag.NoError = true;

        //    User CurUser = context.Users.SingleOrDefault(user => user.Id == curUserId);
        //    CardSet NewCardSet = new CardSet();
        //    NewCardSet.UserOwnerId = (int)CurUser.Id;

        //    return View(NewCardSet);
        //}

        //[HttpPost]
        //public IActionResult CreateSet(CardSet newCard)  //WORKING W/ DB
        //{
        //    ViewBag.NoError = false;

        //    if(newCard.Name != null)
        //    {
        //        UserToCardSet setToAdd = new UserToCardSet();
        //        setToAdd.UserId = newCard.UserOwnerId;
        //        setToAdd.CardSetId = newCard.Id;

        //        //Add CardSet "CardSets" table
        //        context.CardSets.Add(newCard);
        //        context.SaveChanges(); //REQUIRED (in order to give correct id to "UserToCardSet" Table)

        //        //Add CardSet to "UserToCardSet" table
        //        List<CardSet> UserSets = context.CardSets.Where(set => set.UserOwnerId == newCard.UserOwnerId).ToList();
        //        setToAdd.CardSetId = UserSets.LastOrDefault().Id;
        //        context.UserToCardSet.Add(setToAdd);
        //        context.SaveChanges();

        //        return RedirectToAction("ViewSets", "Personal", new{curUserId = newCard.UserOwnerId});
        //    }
        //    return View(newCard);
        //}

        //[HttpGet]
        //public IActionResult EditSet(int CardSetId)
        //{
        //    CardSet CurSet = new CardSet();
        //    CurSet = context.CardSets.SingleOrDefault(set => set.Id == CardSetId);
        //    CurSet.Cards = context.Cards.Where(card => card.CardSetId == CardSetId).ToList();

        //    return View(CurSet);
        //}

        //[HttpPost]
        //public IActionResult DeleteSet(int setToDeleteId)  //WORKING W/ DB
        //{
        //    //NOTE: Could using "Where" cause a crash if there are no cards in the set?
        //    List<Card> CardsToDelete = new List<Card>();
        //    CardsToDelete = context.Cards.Where(card => card.CardSetId == setToDeleteId).ToList();

        //    CardSet SetToDelete = new CardSet();
        //    SetToDelete = context.CardSets.SingleOrDefault(set => set.Id == setToDeleteId);

        //    UserToCardSet UserToSet = new UserToCardSet();
        //    UserToSet = context.UserToCardSet.SingleOrDefault(mapOne => mapOne.CardSetId == setToDeleteId);

        //    //NOTE: Could using "Where" cause a crash if there are no cards in the set?
        //    List<CardsSetToCards> CardsToDeleteTwo = new List<CardsSetToCards>();
        //    CardsToDeleteTwo = context.CardsSetToCards.Where(mapTwo => mapTwo.CardSetId == setToDeleteId).ToList();

        //    if(CardsToDelete != null)
        //    {
        //        for(int i = 0; i < CardsToDelete.Count; i++)
        //            context.Cards.Remove(CardsToDelete.ElementAt(i));
        //    }

        //    context.CardSets.Remove(SetToDelete);
        //    context.UserToCardSet.Remove(UserToSet);

        //    if(CardsToDeleteTwo != null)
        //    {
        //        for(int i = 0; i < CardsToDeleteTwo.Count; i++)
        //            context.CardsSetToCards.Remove(CardsToDeleteTwo.ElementAt(i));
        //    }
        //    context.SaveChanges();

        //    return RedirectToAction("ViewSets", "Personal", new {curUserId = SetToDelete.UserOwnerId});
        //}

        //[HttpGet]
        //public IActionResult ReviewSet(int cardSetId)
        //{
        //    CardSet CurSet = new CardSet();
        //    CurSet = context.CardSets.SingleOrDefault(set => set.Id == cardSetId);
        //    CurSet.Cards = context.Cards.Where(cards=> cards.CardSetId == cardSetId).ToList();

        //    return View(CurSet);
        //}


        //[HttpPost]
        //public IActionResult AddCardToSet(CardSet curSet, string cardDef, string cardExplanation)
        //{
        //    ViewBag.NoError = false;
        //    if((cardDef != null) && (cardExplanation != null))
        //    {
        //        ViewBag.NoError = true;
        //        Card NewCard = new Card();
        //        NewCard.CardSetId = curSet.Id;
        //        NewCard.UserOwnerId = curSet.UserOwnerId;
        //        NewCard.Definition = cardDef;
        //        NewCard.Explanation = cardExplanation;

        //        context.Cards.Add(NewCard);
        //        context.SaveChanges();


        //        List<Card> CurUserCards = new List<Card>();
        //        CurUserCards = context.Cards.Where(card => card.UserOwnerId == NewCard.UserOwnerId).ToList();
        //        CurUserCards.Reverse();
        //        Card NewestAddedCard = CurUserCards.FirstOrDefault();


        //        CardsSetToCards map = new CardsSetToCards();
        //        map.CardId = NewestAddedCard.Id;
        //        map.CardSetId = NewestAddedCard.CardSetId;
        //        context.CardsSetToCards.Add(map);
        //        context.SaveChanges();

        //        return RedirectToAction("ViewSets", "Personal", new {curUserId = NewCard.UserOwnerId});


        //    }
        //    return View();
        //}

        //[HttpGet]
        //public IActionResult EditCard(int curSetId, int cardToEditID) //WORKING W/ DB
        //{
        //    ViewBag.NoError = true;
        //    CardSet curSet = new CardSet();
        //    curSet = context.CardSets.SingleOrDefault(set => set.Id == curSetId);

        //    Card CurCard = new Card();
        //    CurCard = context.Cards.SingleOrDefault(card => card.Id == cardToEditID);

        //    return View(CurCard);
        //}

        //[HttpPost]
        //public IActionResult EditCard(Card curCard)  //WORKING W/ DB
        //{
        //    ViewBag.NoError = false;
        //    if((curCard.Definition != null) && (curCard.Explanation != null))
        //    {
        //        ViewBag.NoError = true;

        //        context.Cards.Single(card => card.Id == curCard.Id).Definition = curCard.Definition;
        //        context.Cards.Single(card => card.Id == curCard.Id).Explanation = curCard.Explanation;
        //        context.SaveChanges();

        //        return RedirectToAction("ViewSets", "Personal", new {curUserId = curCard.UserOwnerId});
        //    }

        //    return View(curCard);
        //}

        //[HttpPost]
        //public IActionResult DeleteCard(int cardToDeleteId) //WORKING W/ DB
        //{
        //    if(cardToDeleteId != 0)
        //    {
        //        Card CardToRemove = new Card();
        //        CardToRemove  = context.Cards.SingleOrDefault(card => card.Id == cardToDeleteId);

        //        CardsSetToCards mapToRemove = context.CardsSetToCards.SingleOrDefault(map => map.CardId == cardToDeleteId);

        //        context.Cards.Remove(CardToRemove);
        //        context.CardsSetToCards.Remove(mapToRemove);
        //        context.SaveChanges();
        //        return RedirectToAction("ViewSets","Personal", new {curUserId = CardToRemove.UserOwnerId});
        //    }

        //    return View();
        //}
    }
    
}
