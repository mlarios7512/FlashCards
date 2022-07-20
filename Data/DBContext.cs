using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using FlashCards.Models;

namespace FlashCards
{
    public class DBContext: DbContext
    {   
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=FlashCards;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Card> Cards {get; set;}
        public DbSet<CardSet> CardSets {get; set;}
        public DbSet<User> Users {get; set;}

        //------------MAPPING ONLY MODELS (below)-----------------------

        public DbSet<UserToCardSet> UserToCardSet {get; set;}
        public DbSet<CardsSetToCards> CardsSetToCards {get; set;}
    }
}