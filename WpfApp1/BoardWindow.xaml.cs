using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BingoTracker
{
    public partial class BoardWindow : Window
    {
        private BingoBoard board;

        public BoardWindow(BingoBoard board)
        {
            InitializeComponent();
            this.board = board;
            Title = $"Plade: {board.ID}";
            BuildGrid();
        }

        private void BuildGrid()
        {
            BoardGrid.Children.Clear();

            for (int r = 0; r < 3; r++)
            {
                bool rowDone = board.IsRowCompleted(r);

                for (int c = 0; c < 9; c++)
                {
                    var tb = new TextBlock
                    {
                        Text = board.GetNumber(r, c)?.ToString() ?? "",
                        FontSize = 16,
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    var border = new Border
                    {
                        Child = tb,
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(2),
                        BorderThickness = new Thickness(1),
                        BorderBrush = rowDone
                                          ? Brushes.Gold
                                          : Brushes.Gray,
                        Background = board.IsMarked(r, c)
                                          ? Brushes.LightGreen
                                          : Brushes.Transparent
                    };

                    BoardGrid.Children.Add(border);
                }
            }
        }
    }
}
