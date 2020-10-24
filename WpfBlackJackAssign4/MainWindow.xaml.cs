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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfBlackJackAssign4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameState gameState;
        private string currentPlayer;
        private List<string> playerList = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            gameState = GameState.WAIT;
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            playerIdTextBlock.Text = "";
            playerScoreTextBlock.Text = "";
            deckTextBlock.Text = "";
            dealerScoreTextBlock.Text = "";
            playerNbrTextBlock.Text = "";
            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
            setupGame(Int32.Parse(newGameWindow.playerNbrText.Text), Int32.Parse(newGameWindow.deckNbrText.Text)); //refine, ersätt med data från backend?
            gameState = GameState.PLAY;
            enableButtons();
            firstRound();

        }

        private void updatePlayerRound(string playerId)
        {
            playerIdTextBlock.Text = playerId;
        }

        private void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            //what to do?
        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            string card = "10C";
            Uri fileUri = new Uri("/pictures/10C.png", UriKind.Relative);
            CardL1.Source = new BitmapImage(fileUri);

            // "/WpfBlackJackAssign4;component/pictures/10C.png


        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            gameOver("dealer", false);
        }

        private void highscoreButton_Click(object sender, RoutedEventArgs e)
        {
            HighscoreWindow highscoreWindow = new HighscoreWindow();
            highscoreWindow.ShowDialog();
        }

        private List<string> setupGame(int nbrOfPlayers, int nbrOfDecks)
        {
            int currentPlayerInt = 1;
            playerNbrTextBlock.Text = nbrOfPlayers.ToString();
            deckTextBlock.Text = nbrOfDecks.ToString();

            for (int i = 0; i<nbrOfPlayers; i++)
            {
                playerList.Add(currentPlayer);
                currentPlayerInt++;
            }

            return playerList;
        }

        private void enableButtons()
        {
            if (gameState == GameState.PLAY)
            {
                hitButton.IsEnabled = true;
                standButton.IsEnabled = true;
                shuffleButton.IsEnabled = true;
            }

            if (gameState == GameState.WAIT)
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
                shuffleButton.IsEnabled = false;
            }
        }

        private void dealersRound()
        {
            gameState = GameState.WAIT;
            enableButtons();
        }

        private void gameOver(string winner, bool isPlayerWinner) //eller int?
        {
            //serialize results

            if (isPlayerWinner)
            {
                winnerTextBlock.Text = $"Player {winner} is the winner!";
            } else
            {
                winnerTextBlock.Text = "The dealer is the winner!";
            }
        }

        private void firstRound()
        {
            //set up dealers cards
            string dCardL1 = "4D"; //get card
            Uri fileUriL1 = new Uri($"/pictures/1{dCardL1}.png", UriKind.Relative);
            CardL1.Source = new BitmapImage(fileUriL1);
           /* Uri fileUriL2 = new Uri("/pictures/8C.png", UriKind.Relative);
            CardL2.Source = new BitmapImage(fileUriL2);*/

            //set up players cards
            Uri fileUriR1 = new Uri("/pictures/4HC.png", UriKind.Relative);
            CardR1.Source = new BitmapImage(fileUriR1);
           /* Uri fileUriR2 = new Uri("/pictures/9S.png", UriKind.Relative);
            CardR2.Source = new BitmapImage(fileUriR2);*/
        }
    }
}
