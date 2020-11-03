using CardDLL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using UtilitiesLib;

namespace WpfBlackJackAssign4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {

        public List<TestResultHighscore> testHigshscoreList { get; set; }

        public HighscoreWindow(List<Player> Players)
        {
            InitializeComponent();

            testHigshscoreList = new List<TestResultHighscore>();


            foreach(Player p in Players)
            {
                testHigshscoreList.Add(new TestResultHighscore
                {
                    Id = p.PlayerID.ToString(),
                    Name = p.Name,
                    Wins = p.wins.ToString(),
                    Losses = p.losses.ToString()
                    //add banked amount

                }) ; ;
            }
            DataContext = this;


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            var ofd = new SaveFileDialog();
            ofd.Filter = "TXT files (*.txt)|*.txt";
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\DataFiles\\";

            if (ofd.ShowDialog() == true)
            {
               // UtilitiesLib.Serialize<TestResultHighscore>.BinarySaveList(ofd.FileName, testHigshscoreList);
            }

            
        }
    }
}
