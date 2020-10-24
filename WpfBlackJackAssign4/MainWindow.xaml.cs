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
    
        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            gameOver("dealer", false);
        }

        private void highscoreButton_Click(object sender, RoutedEventArgs e)
        {
            //

            List<TestResultHighscore> testHigshscoreList = new List<TestResultHighscore>();

            TestResultHighscore t1 = new TestResultHighscore();
            t1.Id = "1";
            t1.Losses = "0";
            t1.Wins = "4";

            TestResultHighscore t2 = new TestResultHighscore();
            t2.Id = "4";
            t2.Losses = "9";
            t2.Wins = "2";

            testHigshscoreList.Add(t1);
            testHigshscoreList.Add(t2);

            HighscoreWindow highscoreWindow = new HighscoreWindow(testHigshscoreList);
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
    }
}
