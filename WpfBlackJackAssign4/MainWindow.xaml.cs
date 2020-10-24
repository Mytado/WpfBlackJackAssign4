using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CardDLL;
using Microsoft.VisualBasic;

namespace WpfBlackJackAssign4
{

    public delegate bool LessThanSeventeen();
    public delegate bool LessThanTwentyOne();


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameState gameState;
        private int currentPlayer;
        //private List<string> playerList = new List<string>();
        private List<Player> Players = new List<Player>();
        private bool[] DonePlayers;
        private Dealer dealer;
        private Deck CardDeck;
        private bool DealerIsDone = false;

        private int noOfPlayers;
        private int noOfDecks;

        private Image[] playerCardImages;
        private Image[] dealerCardImages;


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
            noOfPlayers = Int32.Parse(newGameWindow.playerNbrText.Text);
            noOfDecks = Int32.Parse(newGameWindow.deckNbrText.Text);
            setupGame(true); //refine, ersätt med data från backend?

        }

        private void updatePlayerRound(string playerId)
        {
            playerIdTextBlock.Text = playerId;
        }

        private void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            CardDeck.Shuffle();
        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            int flippedCards = 0;
            foreach (Card c in Players.ElementAt(currentPlayer).hand.cards)
            {
                if (!c.FaceUp)
                {
                    c.Flip();
                    break;
                }
                else
                {
                    flippedCards++;
                }
            }
            if (flippedCards == 5)
            {
                DonePlayers[currentPlayer] = true;
                NextPlayer();
            }

            //Change for Event/Delegate
            if (Players.ElementAt(currentPlayer).IsThick)
            {
                DonePlayers[currentPlayer] = true;
                UpdateCardGraphics();
                MessageBox.Show("Bust");
                NextPlayer();
            }
            else UpdateGame();
        }

        private void NextPlayer()
        {
            if (currentPlayer + 1 < Players.Count)
            {
                currentPlayer++;
                hitButton.IsEnabled = true;
                standButton.IsEnabled = true;
                UpdateCardGraphics();
            }
            else
            {
                UpdateGame();
            }
        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            DonePlayers[currentPlayer] = true;
            NextPlayer();
            UpdateCardGraphics();
        }


        private void highscoreButton_Click(object sender, RoutedEventArgs e)
        {
            if (Players.Count > 0)
            {
                HighscoreWindow highscoreWindow = new HighscoreWindow(Players);
                highscoreWindow.ShowDialog();
            }
        }

        public void dealer_DealerIsDone()
        {
            UpdateCardGraphics();
            calculateResult();
        }

        private void setupGame(bool newGame)
        {
            int nbrOfDecks = noOfDecks;
            int nbrOfPlayers = noOfPlayers;
            dealer = new Dealer("Dealer", 0);
            dealer.DealerIsDone += dealer_DealerIsDone;


            CardDeck = new Deck(nbrOfDecks);
            DonePlayers = new bool[nbrOfPlayers];

            Hand dealerHand = new Hand();
            for (int i = 0; i < 5; i++)
            {
                dealerHand.AddCard(CardDeck);
            }
            dealer.hand = dealerHand;

            for (int i = 0; i < nbrOfPlayers; i++)
            {
                Hand hand = new Hand();
                for (int j = 0; j < 5; j++)
                {
                    hand.AddCard(CardDeck);
                }
                String playerName = "";
                if (newGame)
                {
                    while (playerName.Equals(""))
                    {
                        playerName = Interaction.InputBox("Please submit a name for player " + (i + 1).ToString() + ".", "Player" + (i + 1).ToString() + " name", "Player " + (i + 1).ToString());
                    }
                    Players.Add(new Player(playerName, i + 1));
                }
                Players.ElementAt(i).hand = hand;
                DonePlayers[i] = false;
            }
            playerNbrTextBlock.Text = nbrOfPlayers.ToString();
            deckTextBlock.Text = nbrOfDecks.ToString();

            playerCardImages = new Image[5];
            dealerCardImages = new Image[5];


            playerCardImages[0] = CardR1;
            playerCardImages[1] = CardR2;
            playerCardImages[2] = CardR3;
            playerCardImages[3] = CardR4;
            playerCardImages[4] = CardR5;

            dealerCardImages[0] = CardL1;
            dealerCardImages[1] = CardL2;
            dealerCardImages[2] = CardL3;
            dealerCardImages[3] = CardL4;
            dealerCardImages[4] = CardL5;

            gameState = GameState.PLAY;
            DealerIsDone = false;
            enableButtons();
            firstRound();
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

        private void UpdateCardGraphics()
        {
            BitmapImage[] playerImages = Players.ElementAt(currentPlayer).hand.GetBitmapImages();
            BitmapImage[] dealerImages = dealer.hand.GetBitmapImages();

            //set up dealers cards
            for (int i = 0; i < 5; i++)
            {
                playerCardImages[i].Source = playerImages[i];
                dealerCardImages[i].Source = dealerImages[i];
            }
            playerIdTextBlock.Text = (currentPlayer + 1).ToString();
            playerScoreTextBlock.Text = Players.ElementAt(currentPlayer).hand.Score.ToString();
            dealerScoreTextBlock.Text = dealer.hand.Score.ToString();
        }

        private void UpdateGame()
        {


            if (DonePlayers[currentPlayer])
            {
                hitButton.IsEnabled = false;
                standButton.IsEnabled = false;
            }

            bool PlayersAreDone = false;

            for (int i = 0; i < DonePlayers.Length; i++)
            {
                if (!DonePlayers[i])
                {
                    PlayersAreDone = false;
                    break;
                }
                else
                {
                    PlayersAreDone = true;
                }
            }

            if (PlayersAreDone)
            {
                dealer.KeepDrawing();
                UpdateCardGraphics();
                if (gameState != GameState.GAMEOVER)
                {

                    gameState = GameState.GAMEOVER;
                }
            }
            UpdateCardGraphics();
            if (gameState == GameState.GAMEOVER)
            {
                newTurnButton.IsEnabled = true;
            }
        }

        public void calculateResult()
        {
            if (dealer.IsThick)
            {
                foreach (Player p in Players)
                {
                    if (p.IsThick)
                    {
                        p.losses++;
                        MessageBox.Show(p.PlayerID + " : " + p.Name + " lost...");
                    }
                    else
                    {
                        p.wins++;
                        MessageBox.Show(p.PlayerID + " : " + p.Name + " Won!");
                    }
                }
            }
            else
            {
                foreach (Player p in Players)
                {
                    if (p.IsThick || p.hand.Score <= dealer.hand.Score)
                    {
                        p.losses++;
                        MessageBox.Show(p.PlayerID + " : " + p.Name + " lost...");
                    }
                    else
                    {
                        p.wins++;
                        MessageBox.Show(p.PlayerID + " : " + p.Name + " Won!");
                    }
                }
            }
        }

        private void firstRound()
        {
            currentPlayer = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                Players.ElementAt(i).hand.cards.ElementAt(0).Flip();
            }
            dealer.hand.cards.ElementAt(0).Flip();
            UpdateGame();
            playerScoreTextBlock.Text = Players.ElementAt(currentPlayer).hand.Score.ToString();
            dealerScoreTextBlock.Text = dealer.hand.Score.ToString();
            newTurnButton.IsEnabled = false;
        }

        private void newTurnButton_Click_1(object sender, RoutedEventArgs e)
        {
            setupGame(false);
        }
    }
}
