using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace CardDLL
{
    public class Deck
    {
        Stack<Card> cards;

        public Deck(int _cardMultiplier)
        {
            cards = new Stack<Card>();

            for(int i = 0; i < _cardMultiplier; i++)
            {
                FillDeckWithCards();
            }
            Shuffle();
        }

        public void Shuffle()
        {
            Random rnd = new Random();
            var values = cards.ToArray();
            cards.Clear();
            foreach (var value in values.OrderBy(x => rnd.Next()))
                cards.Push(value);
        }

        public void FillDeckWithCards()
        {
            Card.Suites currentSuite;
            currentSuite = Card.Suites.Clubs;
            for (int i = 1; i <= 13; i++)
            {
                cards.Push(new Card(i, currentSuite));
            }
            currentSuite = Card.Suites.Diamonds;
            for (int i = 1; i <= 13; i++)
            {
                cards.Push(new Card(i, currentSuite));
            }
            currentSuite = Card.Suites.Hearts;
            for (int i = 1; i <= 13; i++)
            {
                cards.Push(new Card(i, currentSuite));
            }
            currentSuite = Card.Suites.Spades;
            for (int i = 1; i <= 13; i++)
            {
                cards.Push(new Card(i, currentSuite));
            }
        }


        public Card draw()
        {
            return cards.Pop();
        }
    }
}
