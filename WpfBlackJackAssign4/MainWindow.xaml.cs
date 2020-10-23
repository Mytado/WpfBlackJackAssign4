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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGameWindow newGameWindow = new NewGameWindow();
            newGameWindow.ShowDialog();
        }

        private void shuffleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you clicked me at shuffle button");
        }

        private void hitButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you clicked me at hit button");
        }

        private void standButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("you clicked me at stand button");
        }

        public class testPlayer
        {
            public int playerId { get; set; }
        }
    }
}
