using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBlackJackAssign4
{
    public class TestResultHighscore
    {
        private string wins;
        private string losses;
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Wins
        {
            get { return wins; }
            set { wins = value; }
        }

        public string Losses
        {
            get { return losses; }
            set { losses = value; }
        }
    }
}
