using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class DeckContainer
    {
        public List<Card> Cards { get; set; } = new List<Card>();

        public DeckContainer()
        {
            this.Cards = CreateDeck();
            this.Cards = ShuffleDeck(this.Cards);
        }
        private List<Card> CreateDeck()
        {
            var deck = new List<Card>();

            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    deck.Add(new Card(s, r));
                }
            }
            return deck;
        }

        private List<Card> ShuffleDeck(List<Card> cards)
        {
            //sort the deck. NOTICE that the variable 'deck' is unchanged, but 'randomDeck' is the actual sorted deck.
            var randomDeck = cards.OrderBy(x => Guid.NewGuid()).ToList();

            return randomDeck;
        }



        public Card DealCard()
        {
            var card = this.Cards[0];
            this.Cards.RemoveAt(0);
            return card;
        }

        public override string ToString()
        {
            var rv = new StringBuilder();
            this.Cards.ForEach(x =>
            {
                rv.Append(x + "\n");
            });
            return rv.ToString();
        }
    }

}
