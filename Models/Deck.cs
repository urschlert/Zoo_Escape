using System;
using System.Collections.Generic;

namespace ZooBreakout.Models 
{

    /*
        This class represents the cards that are
        layed out on the board. It is used to keep track of the
        game status and moves.
    */

    public class Deck
    {
        public List<int> Cards { get; set; } = new List<int>();
        public List<int> InitCards { get; set; } = new List<int>();
        public int LastCardTouched { get; set; } = -1;

        // constructor for infinite play
        public Deck(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++) 
            {
                Random r = new Random();
                Cards.Add(r.Next(0, 2));
            }
            InitCards = new List<int>(Cards);
        }

        // constructor used for the first and second round razor pages
        public Deck(int roundpage, int round)
        {
            // page one has a set 5 games
            if (roundpage == 1)
            {
                switch(round)
                {
                    case 0:
                        Cards = new List<int> {0, 1, 1, 1, 1}; break;
                    case 1:
                        Cards = new List<int> {0, 1, 1, 0, 0}; break;
                    case 2:
                        Cards = new List<int> {0, 0, 1, 0, 1}; break;
                    case 3:
                        Cards = new List<int> {0, 0, 0, 0, 0}; break;
                    case 4:
                        Cards = new List<int> {0, 1, 1, 1, 0}; break;
                    default: break;
                }
            }
            // page 2 has a set 5 games
            else
            {
                switch(round)
                {
                    case 0:
                        Cards = new List<int> {0, 1, 1, 1, 1, 0, 0}; break;
                    case 1:
                        Cards = new List<int> {0, 1, 1, 0, 0, 1, 0}; break;
                    case 2:
                        Cards = new List<int> {0, 0, 0, 0, 1, 0, 1}; break;
                    case 3:
                        Cards = new List<int> {0, 0, 0, 0, 0, 0, 0}; break;
                    case 4:
                        Cards = new List<int> {0, 1, 1, 1, 0, 0, 0}; break;
                    default: break;
                }
            }
            InitCards = new List<int>(Cards);
        }

        // flips all the corresponding cards
        public void ChangeCard(int card)
        {
            if (Cards[card] == 0)   // if the card isn't face down
            {
                Cards[card] = 2;
                if (card != 0)      // if the card isn't in the far left position
                {
                    if (Cards[card-1] != 2)
                        Cards[card-1] = 1 - Cards[card-1];
                }
                if (card != Cards.Count-1)  // if the card isn't in the far right position
                {
                    if (Cards[card+1] != 2)
                        Cards[card+1] = 1 - Cards[card+1];
                }
            }
            LastCardTouched = card; // record the last changed card
        }

        // returns if deck is empty
        private bool DeckEmpty()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] == 0 || Cards[i] == 1)
                    return false;
            }
            return true;
        }

        // determines if a win is true
        public bool CheckWin()
        {
            return DeckEmpty();
        }

        // determines if it is possible for the player to win
        public bool WinPossible(int option)
        {
            int faceUp = 0;
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] == 0)
                    faceUp++;
            }
            if (option == 1)
                return (faceUp % 2 != 0 && faceUp != 0);
            else
                return (faceUp != 0);
        }

        // prints the deck to the console
        public void PrintDeck()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Console.WriteLine(Cards[i]);
            }
        }

        // resets the deck to its initial state
        public void ResetDeck()
        {
            Cards = new List<int>(InitCards);
        }

        // undoes the last move
        public void UndoLastMove()
        {
            // if there's been a move
            if (LastCardTouched != -1)
            {
                // revert changes back
                Cards[LastCardTouched] = 0;
                if (LastCardTouched != 0)
                    Cards[LastCardTouched-1] = 1 - Cards[LastCardTouched-1];
                if (LastCardTouched != Cards.Count-1)
                    Cards[LastCardTouched+1] = 1 - Cards[LastCardTouched+1];
                LastCardTouched = -1;
            }
        }
    }
}