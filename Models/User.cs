using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FlashCards.Models
{
    public class User
    {
        public const int MAX_CARDS_PER_SET = 5;
        public int Id { get; set; }

        [Required]
        public string Email {get; set;}


        [Required(ErrorMessage ="A username is required")]
        public string Username {get; set;}


        [Required(ErrorMessage ="A password is required")]
        public string Password {get; set;}

        public List<CardSet> MySets { get; set; } = new List<CardSet>(MAX_CARDS_PER_SET);

        public User() 
        {
            MySets = new List<CardSet>(MAX_CARDS_PER_SET);
        }

        public int GetMaxCardPerSet()
        {
            return MAX_CARDS_PER_SET;
        }
    }
}
