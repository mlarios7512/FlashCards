using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Models
{
    [Table("User")]
    public partial class User
    {
        const int MAX_CARDS_PER_SET = 20;
        public User()
        {
            CardSets = new HashSet<CardSet>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [StringLength(50)]
        public string Password { get; set; } = null!;

        [InverseProperty("UserOwner")]
        public ICollection<CardSet> CardSets { get; set; } = new List<CardSet>(MAX_CARDS_PER_SET);

        public int GetMaxCardsPerSet() 
        {
            return MAX_CARDS_PER_SET;
        }
    }
}
