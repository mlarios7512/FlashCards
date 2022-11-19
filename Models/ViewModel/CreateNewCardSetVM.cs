using System.ComponentModel.DataAnnotations;

namespace FlashCards.Models.ViewModel
{
    public class CreateNewCardSetVM
    {
        public int CurUserId {get;set;}

        [Required (AllowEmptyStrings = false)]
        public string SetName {get;set;}

        [Required (AllowEmptyStrings = false)]
        public string[] CardFrontSide { get; set; }
        [Required (AllowEmptyStrings = false)]
        public string[] CardBackSide { get; set; }

        public CreateNewCardSetVM() { }
        public CreateNewCardSetVM(int newCardAmount) 
        {
            CardFrontSide = new string[newCardAmount];
            CardBackSide = new string[newCardAmount];
        }
    }
}
