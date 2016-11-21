using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Hand
    {
        public Hand(string who, Card card1, Card card2)
        {
            this.Who = who;
            this.Cards.Add(card1);
            this.Cards.Add(card2);
        }

        public List<Card> Cards { get; set; } = new List<Card>();
        public string Who { get; set; }

        public int Count()
        {
            var sum = this.Cards.Sum(x => x.GetCardValue());
            return sum;
        }

        public string ShowDealerHand()
        {
            return this.Cards.First().ToString();
        }

        public override string ToString()
        {
            var _rv = $"{this.Who}'s hand is \n";
            this.Cards.ForEach(c =>
            {
                _rv += $"{c} \n";
            });

            _rv += $"Total {this.Count()}";
            return _rv;
        }

    }

}
