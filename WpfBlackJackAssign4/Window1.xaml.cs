using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfBlackJackAssign4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {

        public List<TestResultHighscore> testHigshscoreList { get; set; }

        public HighscoreWindow()
        {
            InitializeComponent();

            testHigshscoreList = new List<TestResultHighscore>();

            TestResultHighscore t1 = new TestResultHighscore
            {
                Id = "1",
                Losses = "0",
                Wins = "4"
            };

            TestResultHighscore t2 = new TestResultHighscore
            {
                Id = "4",
                Losses = "9",
                Wins = "2"
            };

            testHigshscoreList.Add(t1);
            testHigshscoreList.Add(t2);

            DataContext = this;


            }


        }
    }
