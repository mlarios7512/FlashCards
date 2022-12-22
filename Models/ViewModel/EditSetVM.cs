using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.ViewModel
{
    public class EditSetVM
    {
        public int MaxCardsPerSet { get; set; } 
        public int SetId { get; set; }
        public string SetName { get; set; }
        public int[] CardIDsInSet { get; set; }
      
        public string[] FrontCard { get; set; }
        public string[] BackCard { get; set; }
        public bool[] CardsToDelete { get; set; }

        public EditSetVM() 
        {

        }

        public EditSetVM(int numCardsInSet) 
        {
            FrontCard = new string[numCardsInSet];
            BackCard = new string[numCardsInSet];
            CardIDsInSet = new int[numCardsInSet];
            
        }
    }
}
