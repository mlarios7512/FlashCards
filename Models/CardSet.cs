using System;

namespace FlashCards.Models

{
    public class CardSet
    {
        public int Id { get; set; }
        public int UserOwnerId {get; set;}
        public string Name { get; set; }
        public List<Card> Cards {get; set;} = new List<Card>(10);
        public DateTime BestTime = new DateTime();

        public CardSet()
        {

        }

        public CardSet(string setName) 
        {
            Name = setName;
        }

}
}
