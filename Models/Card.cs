using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Models
{
    public partial class Card
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(300)]
        [Required(AllowEmptyStrings = false)]
        public string FrontCard { get; set; } = null!;
        [StringLength(300)]
        [Required(AllowEmptyStrings = false)]
        public string BackCard { get; set; } = null!;
        public int CardSetId { get; set; }

        [ForeignKey("CardSetId")]
        [InverseProperty("Cards")]
        public virtual CardSet CardSet { get; set; } = null!;
    }
}
