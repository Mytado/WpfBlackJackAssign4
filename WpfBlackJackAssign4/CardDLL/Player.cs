using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDLL
{
    public class Player
    {
        public bool IsFinished;

        public bool IsThick { get => hand.Score > 21; }

        public string Name { get; private set; }

        public int PlayerID { get; private set; }

        public int wins;
        public int losses;

        public Hand hand;

        public Player(string _name, int _id)
        {
            wins = 0;
            losses = 0;
            IsFinished = false;
            Name = _name;
            PlayerID = _id;
        }


    }
}
