using FlashCards.Models;
using FlashCards.DAL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using FlashCards.DAL.Concrete;

namespace FlashCards.DAL.Concrete
{
    public class CardSetRepository: Repository<CardSet>, ICardSetRepository
    {
        public CardSetRepository(FlashCardsDBContext context): base(context)
        {
   
        }

        public int GetMaxCardsAllowed() 
        {
            return 20;
        }
    }
}
