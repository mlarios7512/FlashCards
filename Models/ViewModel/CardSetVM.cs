using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.ViewModel
{
    public class CardSetVM
    {
        const int MAX_CARDS_PER_SET = 20;

        //---------------EXISTING CARDS (below)---------------
        public int CurUserId { get; set; }
        [StringLength(50)]
        public string? SetName { get; set; } = null;

        public List<Card> ExistingCards = new List<Card>(MAX_CARDS_PER_SET);
        public bool[] DeleteExistingCards = new bool[MAX_CARDS_PER_SET];
        //---------------EXISTING CARDS (above)---------------

        public string []PotentialFronts = new string[MAX_CARDS_PER_SET];
        public string []PotentialBacks = new string [MAX_CARDS_PER_SET];

        public CardSetVM() 
        {
        }

        public int GetMaxCardsPerSet() 
        {
            return MAX_CARDS_PER_SET;
        }
    }
}
