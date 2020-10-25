using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DemoCardDLL
{
    public class Hand
    {
        public Card LastCard;

        public Stack<Card> cards = new Stack<Card>();
        public int NumberOfCards {get => cards.Count; }

        public int Score { get => CardScore(); }

        public Hand()
        {
            
        }

        private int CardScore()
        {
            int res = 0;
            foreach(Card c in cards)
            {
                if (c.FaceUp) { 
                    res += c.Value;
                }
            }
            return res;
        }

        public void AddCard(Deck d)
        {
            Card c = null;
            bool cardDuplicate = true;
            if(cards.Count == 0)
            {
                c = d.draw();
            }
            else {
                while (cardDuplicate)
                {
                    c = d.draw();
                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (cards.ElementAt(i).ToString().Equals(c.ToString()))
                        {
                            cardDuplicate = true;
                            break;
                        }
                        else
                        {
                            cardDuplicate = false;
                        }
                    }
                }
            }
            cards.Push(c);
        }


        public BitmapImage[] GetBitmapImages()
        {
            BitmapImage[] images = new BitmapImage[5];

            for (int i = 0; i < 5; i++)
            {
                images[i] = cards.ElementAt(i).GetImage();
            }

            return images;
        }

        public void Clear()
        {
            cards.Clear();
        }

    }
}
