using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        /// <summary>
        /// holds the current value of the cells in the active game
        /// </summary>
        private MarkType[] mResults;

        /// <summary>
        /// true if first players turn (x)
        /// </summary>
        private bool mPlayer1Turn;

        /// <summary>
        /// true if the game has ended
        /// </summary>
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }
        #endregion

        /// <summary>
        /// starting a new game after setting back everything to default values
        /// </summary>

        private void NewGame() 
        {
            //creating a blank array for the free cells
            mResults = new MarkType[9];

            // by default marktype 'Free'
            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            //player1 starts the game
            mPlayer1Turn = true;

            //make the board blank
            //iterate thru every bottom on the grid and clearing them
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                //change content, background & foreground to default values
                button.Content = string.Empty;
                button.Background = Brushes.WhiteSmoke;
                button.Foreground = Brushes.Black;
            });

            //the game is on 
            mGameEnded = false;
        }

        /// <summary>
        /// handles button click events
        /// </summary>
        /// <param name="sender">the button that was clicked</param>
        /// <param name="e">the events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //start a new game at the first click after the previous game has been ended
            if(mGameEnded)
            {
                NewGame();
                return;
            }

            //cast sender to button
            var button = (Button)sender;

            //returns the column the button is on
            var column = Grid.GetColumn(button);
            //return the row the button is on
            var row = Grid.GetRow(button);

            //find the buttons position in the array
            //column number + (row * number of columns)
            var index = column + (row * 3);

            if(mResults[index] != MarkType.Free)
            {
                //do nothing if the cell already has a value in it
                return;
            }

            //set the cell value based on which players turn is it
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            /*
            if (mPlayer1Turn)
                mResults[index] = MarkType.Cross;
            else
                mResults[index] = MarkType.Nought;
            */

            //set button text
            button.Content = mPlayer1Turn ? "X" : "O";

            //change noughts colour
            if(!mPlayer1Turn)
                button.Foreground = Brushes.DarkCyan;

            //toggle the players turns
            //bitwise operator the change boolean value
            mPlayer1Turn ^= true;

            //checking for a winner
            CheckForWinner();

          
        }

        /// <summary>
        /// check if there is a 3 line straight of one symbol
        /// </summary>
        private void CheckForWinner()
        {
            //check manually for all 8 ways of winning

            #region horizontal wins
            //check for horizontal wins
            //row0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGray;
            }

            //row1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGray;
            }

            //row2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGray;
            }
            #endregion


            #region vertical wins
            //check for vertical wins
            //column0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGray;
            }

            //column1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGray;
            }

            //column2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGray;
            }
            #endregion


            #region diagonal wins
            //check for diagonal wins
            //top left - bottom right
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGray;
            }

            //top right - bottom left
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                //game ends
                mGameEnded = true;

                //highlight winning cells
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.LightGray;
            }

            #endregion

            #region draw
            //all cells are taken and no winner (draw)
            if (!mResults.Any(f => f == MarkType.Free))
            {
                mGameEnded = true;

                //show its a draw
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.LightGray;
                });

            }

            #endregion
        }
    }
}
