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
        private testPlayer TestPlayer { get => this.TestPlayer; set => this.TestPlayer = value; }
        private string winnerString;
        private string currentPlayer;
        private List<string> playerList = new List<string>();


        public MainWindow()
        {
            InitializeComponent();
            gameState = GameState.WAIT;
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
            setupGame(Int32.Parse(newGameWindow.playerNbrText.Text)); //refine, ersätt med data från backend?
            gameState = GameState.PLAY;
            enableButtons();

            //test
            
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
            
        }

        private void highscoreButton_Click(object sender, RoutedEventArgs e)
        {
            //
            HighscoreWindow highscoreWindow = new HighscoreWindow();
            highscoreWindow.ShowDialog();
        }

        private List<string> setupGame(int nbrOfPlayers)
        {
            int currentPlayerInt = 1;

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

        private void updateGui()
        {

        }

        public class testPlayer
        {
            public int playerId { get; set; }
        }

        private void gameOver()
        {
            //serialize results
        }

        
    }
}
