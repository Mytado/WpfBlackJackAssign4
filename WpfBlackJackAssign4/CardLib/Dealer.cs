using System;
using System.Collections.Generic;
using System.Text;

namespace DemoCardDLL
{
    public delegate void Notify();

    public class Dealer : Player
    {
        public bool done;

        public event Notify DealerIsDone;

        public Dealer(string _name, int _id) : base(_name,_id)
        {
            done = false;
        }

        public void KeepDrawing()
        {
            while (!done)
            {

                if (hand.Score > 16)
                {
                    done = true;
                }
                else
                {
                    foreach (Card c in hand.cards)
                    {
                        if (!c.FaceUp)
                        {
                            c.Flip();
                            break;
                        }
                    }
                }
            }
            DealerIsDone?.Invoke();
        }
    }
}
