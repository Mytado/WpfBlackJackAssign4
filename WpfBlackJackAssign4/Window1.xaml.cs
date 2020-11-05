using CardDLL;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace WpfBlackJackAssign4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class HighscoreWindow : Window
    {

        int fundsFilter = 0;
        string nameFilter = "";

        private bool PlayersHaveBeenDeleted = false;
        List<Player> Players = new List<Player>();
        public List<TestResultHighscore> testHigshscoreList { get; set; }

        public HighscoreWindow()
        {
            InitializeComponent();
            UpdateList();
        }

        private void UpdateList()
        {
            /*
            string nameFilter = NameFilter.Text;

            if (!int.TryParse(FundsFilter.Text, out int fundsFilter))
            {
                MessageBox.Show("Minimum funds only accept numbers.");
                FundsFilter.Text = "0";
                return;
            }
            */

            //Players = Player.GetAllPlayers(nameFilter, fundsFilter);
            Players = Player.GetAllPlayers(nameFilter, fundsFilter);
            testHigshscoreList = new List<TestResultHighscore>();

            Console.WriteLine("Number of players: " + Players.Count);
            foreach (Player p in Players)
            {
                if (p.Name != "Dealer")
                {
                    testHigshscoreList.Add(new TestResultHighscore
                    {
                        Id = p.PlayerID.ToString(),
                        Name = p.Name,
                        Wins = p.wins,
                        Losses = p.losses,
                        Funds = p.funds
                    }); ;
                }
            }

            this.HighscoreList.ItemsSource = testHigshscoreList;
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

        private void HighscoreWindow_Closing(object sender, CancelEventArgs e)
        {
            if (PlayersHaveBeenDeleted)
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

        private void DeletePlayer_Click(object sender, RoutedEventArgs e)
        {
            if (HighscoreList.SelectedIndex != -1)
            {
                Players[HighscoreList.SelectedIndex].DeletePlayer();
                UpdateList();
                PlayersHaveBeenDeleted = true;
            }
        }

        private void FilterByFunds_Click(object sender, RoutedEventArgs e)
        {
            string tmp = Interaction.InputBox("Please insert lowest funds to filter by.");
            if (!int.TryParse(tmp, out fundsFilter))
            {
                MessageBox.Show("Minimum funds only accept numbers.");
                return;
            }
            UpdateList();
        }

        private void FilterByName_Click(object sender, RoutedEventArgs e)
        {
            nameFilter = Interaction.InputBox("Please insert name to filter by.");
            UpdateList();
        }
        /*
        private void FilterChanged(object sender, EventArgs e)
        {
            UpdateList();
        }
        */
    }
}
