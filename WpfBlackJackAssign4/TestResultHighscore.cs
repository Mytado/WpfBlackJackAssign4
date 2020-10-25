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
        public string Wins { get; set; }
        public string Losses { get; set; }
        public string Name { get; set; }
    }
}
