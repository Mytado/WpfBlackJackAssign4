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
        private List<string> highscore = new List<string>();

        public NewGameWindow()
        {
            InitializeComponent();
        }

        //window that shows up after pressing "new game" button in MainWindow that allows the player to enter valid information that's required.
        private void startGameButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean isInputOkay = true;

            try
            {
                nbrOfDecks = Int32.Parse(deckNbrText.Text);
                nbrOfPlayers = Int32.Parse(playerNbrText.Text);

            } catch (Exception)
            {
                isInputOkay = false;
                MessageBox.Show("Please enter valid numbers");
                deckNbrText.Text = "";
                playerNbrText.Text = "";
            }

            

            MessageBox.Show("Number of decks: " + nbrOfDecks +"\n Number of players: " + nbrOfPlayers);
            //alert bakåt med antal spelare och decks, trigger för att skapa spel

            if (isInputOkay)
            {
                this.Close();
            }


        }
    }
}
