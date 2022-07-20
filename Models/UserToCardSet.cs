namespace FlashCards.Models
{
    public class UserToCardSet
    {
        public int Id {get; set;}
        public int UserId {get; set;}
        public int CardSetId {get; set;}

        
        public UserToCardSet()
        {

        }

        public UserToCardSet(int theUserId, int theCardSetId)
        {
            Id = Id;
            UserId = theUserId;
            CardSetId = theCardSetId;
        }
    }
}