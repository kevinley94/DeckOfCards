using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDeck
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck pokerDeck = new Deck();
            pokerDeck.Shuffle(); //calls on the shuffle
            PokerPlayer user = new PokerPlayer();
            Console.WriteLine("Hand: \n-------------- ");
            user.DrawHand(pokerDeck);
            user.checkHandValue();
            user.ReturnHandToDeck(pokerDeck);
            
           
            
            Console.ReadKey();
        }
    }

    class Card
    {

        public enum suits
        {
            Clubs= 1,
            Diamonds,
            Hearts,
            
        }
        public enum rank
        {
            Two = 2,
            Three,
           
        }
        public rank CardRank { get; set; }
        public suits CardSuits { get; set; }

        public void printCard()
        {
            Console.WriteLine("{0} of {1}",CardRank, CardSuits);
        }
        public Card()
        {

        }
        public Card(suits cardsuit, rank cardrank)
        {
            CardRank = cardrank;
            CardSuits = cardsuit;
        }
    }

    class Deck: Card //Inheritance..It allows deck to access everything in value but not vice versa
    {
        public List<Card> listOfCards = new List<Card>();
        public Deck()
        {
            foreach (var suit in Enum.GetNames(typeof(suits)).ToList())
            {
                foreach (var value in Enum.GetNames(typeof(rank)).ToList())
                {
                    listOfCards.Add(new Card((suits)Enum.Parse(typeof(suits), suit), (rank)Enum.Parse(typeof(rank), value)));
                }
            }
        }
        public void Shuffle()
        {
            Random rng = new Random();
            for (int i = listOfCards.Count(); i > 1; i--)
            {
                int j = rng.Next(i);
                Card temp = listOfCards[j];
                listOfCards[j] = listOfCards[i - 1];
                listOfCards[i - 1] = temp;
            }

        }
    }

    class PokerPlayer
    {
        public List<Card> Hand = new List<Card>();
        public PokerPlayer()
        {

        }

        public void DrawHand(Deck pokerDeck)
        {
            while (Hand.Count() != 5)
            {
                Hand.Add(pokerDeck.listOfCards[0]); //Pokerdeck is the class, list of cards is the actual list that it pulls
                pokerDeck.listOfCards.RemoveAt(0); //will remove the card from the deck
            }

            foreach (var item in Hand)
            {
                Console.WriteLine("{0} of {1}", item.CardRank, item.CardSuits);
            }
        }
        public void checkHandValue()
        {
            Card storage = new Card();
            
            if (HasOnePair(ref storage))
            {
                Console.WriteLine("-----------\nPair of {0}'s", storage.CardRank); 
            
            }
            if (HasThreeOfAKind(ref storage))
                {
                   
                    Console.WriteLine("---------\nThree of A Kind: {0}", storage.CardRank);
                }
            
            else
                HasHighCard(storage);
 
            
            
        }
        public void HasHighCard(Card temp)
        {
            foreach (var item in Hand) //this will go through the entire hand..
            {

                if (item.CardRank > temp.CardRank) //if there's a card that's higher in numerical value than whatever was passed in, it becomes the new high card
                {
                    temp = item;
                }
            } Console.WriteLine("--------\nHigh Card: {0} of {1}", temp.CardRank, temp.CardSuits);
           
        }
        public bool HasOnePair(ref Card storage)
        {
            for (int i = 0; i < Hand.Count(); i++) //it will go through each card in the hand
            {
                for (int j = i + 1; j < Hand.Count(); j++)// and for each card..
                {
                    if (Hand[i].CardRank.CompareTo(Hand[j].CardRank) == 0)//it will see if it has a match..if it does..
                    {
                        storage = Hand[i];
                  
                            return true; //it returns true
                    }
                    
                }

            } return false;
            
        }
        public bool HasTwoPairs()
        {
            
            return true;
        }
        public bool HasThreeOfAKind(ref Card storage)
        {
            if (Hand.GroupBy(x => x.CardRank).Any(x => x.Count() == 3))
            {
                return true;
            }
            return false;
        }
        public bool HasStraight()
        {
            return true;
        }
        public bool HasFlush()
        {
            return true;
        }
        public bool HasFullHouse()
        {
            return true;
        }
        public bool HasFourOfAKind()
        {
            return true;
        }
        public bool HasStraightFlush()
        {
            return true;
        }
        public bool HasRoyalFlush()
        {
            return true;
        }

        public void ReturnHandToDeck(Deck pokerDeck)
        {
            foreach (var item in this.Hand)
            {
                pokerDeck.listOfCards.Add(item);
            }
        }
    }
}

