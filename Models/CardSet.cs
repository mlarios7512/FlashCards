using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Models
{
    [Table("CardSet")]
    public partial class CardSet
    {
        const int MAX_CARDS_PER_SET = 20;
        public CardSet()
        {
            Cards = new HashSet<Card>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = null!;
        public int UserOwnerId { get; set; }

        [ForeignKey("UserOwnerId")]
        [InverseProperty("CardSets")]
        public virtual User UserOwner { get; set; } = null!;
        [InverseProperty("CardSet")]
        public virtual ICollection<Card> Cards { get; set; } = new List<Card>(MAX_CARDS_PER_SET);
    }
}
