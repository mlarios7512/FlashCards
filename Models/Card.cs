using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int UserOwnerId {get; set;}
        public int CardSetId {get; set;}

        [Required(ErrorMessage ="Card must have a definition.")]
        public string Definition { get; set;}
        public string Explanation { get; set;}

        public Card()
        {
            
        }

        public Card(string myDef, string myExplanation) 
        {
            Definition = myDef;
            Explanation = myExplanation;
        }

    }
}
