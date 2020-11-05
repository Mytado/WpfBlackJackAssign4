using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfBlackJackAssign4
{
    [Serializable]
    public class TestResultHighscore
    {
        public string Id { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Name { get; set; }
        public int Funds { get; set; }
    }
}
