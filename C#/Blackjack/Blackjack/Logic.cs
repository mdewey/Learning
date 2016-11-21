using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Blackjack
{
   public static class Logic
    {
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
            if (player.Count() >= 21)
            {
                stoppedPlaying = true;
                Console.WriteLine("Black Jack!");
            }
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

    }
}
