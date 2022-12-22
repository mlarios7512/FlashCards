using FlashCards.Models;
using FlashCards.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace FlashCards.DAL.Concrete
{
    public class CardSetRepository: ICardSetRepository
    {
        private DbSet<Card> _cards;
        public CardSetRepository(FlashCardsDBContext context)
        {
            _cards = context.Cards;
        }

        public int GetMaxCardsAllowed() 
        {
            return 20;
        }
    }
}
