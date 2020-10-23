using System;
using System.Collections.Generic;
using System.Data;
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
using UtilitiesLib;

namespace WpfBlackJackAssign4
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewGameWindow : Window
    {
        private int nbrOfPlayers;
        private int nbrOfDecks;

        public NewGameWindow()
        {
            InitializeComponent();
        }

        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                nbrOfDecks = Int32.Parse(deckNbrText.Text);
                nbrOfPlayers = Int32.Parse(playerNbrText.Text);

            } catch (Exception)
            {
                MessageBox.Show("you done fucked up");
            }

            

            MessageBox.Show("Number of decks: " + nbrOfDecks +"/n Number of players: " + nbrOfPlayers);
            //alert bakåt med antal spelare och decks vad som 

            

        }
    }
}
