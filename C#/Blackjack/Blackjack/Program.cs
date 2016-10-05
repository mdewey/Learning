using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {

        public enum Suit
        {
            Hearts,
            Clubs,
            Diamonds,
            Spades
        }

        public enum Rank
        {
            Ace,
            Deuce,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King
        }

        public class Card
        {
            public Suit Suit { get; set; }
            public Rank Rank { get; set; }

            public Card(Suit s, Rank r)
            {
                this.Suit = s;
                this.Rank = r;
            }

            public int GetCardValue()
            {
                var rv = 0;
                switch (this.Rank)
                {
                    case Rank.Ace:
                        rv = 11;
                        break;
                    case Rank.Deuce:
                        rv = 2;
                        break;
                    case Rank.Three:
                        rv = 3;
                        break;
                    case Rank.Four:
                        rv = 4;
                        break;
                    case Rank.Five:
                        rv = 5;
                        break;
                    case Rank.Six:
                        rv = 6;
                        break;
                    case Rank.Seven:
                        rv = 7;
                        break;
                    case Rank.Eight:
                        rv = 8;
                        break;
                    case Rank.Nine:
                        rv = 9;
                        break;
                    case Rank.Ten:
                        rv = 10;
                        break;
                    case Rank.Jack:
                        rv = 10;
                        break;
                    case Rank.Queen:
                        rv = 10;
                        break;
                    case Rank.King:
                        rv = 10;
                        break;
                    default:
                        break;
                }
                return rv;
            }



            public override string ToString()
            {
                return $"The {this.Rank} of {this.Suit}";
            }
        }

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


        public static List<Card> CreateDeck()
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

        public static List<Card> ShuffleDeck(List<Card> cards)
        {
            //sort the deck. NOTICE that the variable 'deck' is unchanged, but 'randomDeck' is the actual sorted deck.
            var randomDeck = cards.OrderBy(x => Guid.NewGuid()).ToList();

            return randomDeck;
        }

        public class DeckContainer
        {
            public List<Card> Cards { get; set; } = new List<Card>();

            public DeckContainer()
            {
                this.Cards = CreateDeck();
                this.Cards = ShuffleDeck(this.Cards);
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

        public static void WriteBreak()
        {
            Console.WriteLine("\n----------------------------\n");
        }

        public static void PlayGame()
        {
            var deck = new DeckContainer();

            if (deck.Cards.Count() < 5)
            {
                // reshuffle deck
                WriteBreak();
                Console.WriteLine("Reshuffling Deck");
                deck = new DeckContainer();
                WriteBreak();
            }

            Console.WriteLine("Let's play Blackjack");

            // Set up hands
            var dealer = new Hand("Dealer", deck.DealCard(), deck.DealCard());
            var player = new Hand("Player", deck.DealCard(), deck.DealCard());

            WriteBreak();
            Console.WriteLine("Dealers Card");
            Console.WriteLine(dealer.ShowDealerHand());


            // Player's turn
            var stoppedPlaying = false;
            while (!stoppedPlaying)
            {
                WriteBreak();
                Console.WriteLine("Your hand:");
                Console.WriteLine(player);

                WriteBreak();

                Console.WriteLine("(H)it or (S)tay");
                var action = Console.ReadLine().ToLower();
                if (action == "h")
                {
                    Console.WriteLine("hitting");
                    var newCard = deck.DealCard();
                    Console.WriteLine($"Next card {newCard}");
                    player.Cards.Add(newCard);
                    if (player.Count() >= 21)
                    {
                        Console.WriteLine("BUSTED!");
                        stoppedPlaying = true;
                    }
                }
                else if (action == "s")
                {
                    Console.WriteLine("staying");
                    stoppedPlaying = true;
                }
                else
                {
                    Console.WriteLine("Pick an action:(h)it or (s)tay ");
                }
            }

            // Dealer's Turn
            while (dealer.Count() < 16)
            {
                WriteBreak();
                Console.WriteLine("Dealer is hitting");
                Thread.Sleep(750);
                var newCard = deck.DealCard();
                Console.WriteLine($"Dealer was delt {newCard}");
                dealer.Cards.Add(newCard);
                Thread.Sleep(750);
            }

            // Game results
            // player and dealer are under 21
            // player is under, dealer is over
            // player is over, dealer is under
            // player is over, dealer is over
            // player is at 21, dealer is XXXX
            // player is XXX, dealer is at 21

            WriteBreak();

            if (player.Count() < 21 && dealer.Count() < 21)
            {
                if (player.Count() > dealer.Count())
                {
                    Console.WriteLine($"Player won with a {player.Count()} over the dealer's {dealer.Count()}");
                }
                else if (player.Count() < dealer.Count())
                {
                    Console.WriteLine($"Dealer won with a {dealer.Count()} over the player's {player.Count()}");
                }
                else
                {
                    Console.WriteLine($"Tie game! Dealer with a {dealer.Count()} and the player has {player.Count()}");
                }
            }
            else if (player.Count() < 21 && dealer.Count() > 21)
            {
                Console.WriteLine($"Player wins {player.Count()}, Dealer busts with a {dealer.Count()}");
            }
            else if (player.Count() > 21 && dealer.Count() < 21)
            {
                Console.WriteLine($"Player busts with a {player.Count()}, Dealer wins with a {dealer.Count()}");
            }
            else if (player.Count() > 21 && dealer.Count() > 21)
            {
                Console.WriteLine("Both player and dealer have busted! No winners");
            }
            else if (player.Count() == 21 && dealer.Count() != 21)
            {
                Console.WriteLine("Player hit 21. Blackjack!");
            }
            else if (player.Count() != 21 && dealer.Count() == 21)
            {
                Console.WriteLine("Dealer hit 21. Blackjack!");
            }
            else if (player.Count() == 21 && dealer.Count() == 21)
            {
                Console.WriteLine("Both hit 21! Winner, Winner");
            }


        }


        static void Main(string[] args)
        {
            var playing = true;
            while (playing)
            {
                WriteBreak();
                PlayGame();
                WriteBreak();
                Console.WriteLine("Play again? Y or N?");
                var again = Console.ReadLine().ToLower();
                if (again == "n")
                {
                    playing = false;
                }
            }
            Console.WriteLine("Goodbye");
            // End 
            Console.ReadLine();

        }
    }
}
