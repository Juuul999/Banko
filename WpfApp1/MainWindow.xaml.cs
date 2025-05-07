using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace BingoTracker
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<BingoBoard> boards = new ObservableCollection<BingoBoard>();

        public MainWindow()
        {
            InitializeComponent();
            BoardsDataGrid.ItemsSource = boards;
        }

        private void AddBoard_Click(object sender, RoutedEventArgs e)
        {
            var id = BoardIdTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(id))
            {
                boards.Add(new BingoBoard(id));
                BoardIdTextBox.Clear();
            }
        }

        private void CallNumber_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CalledNumberTextBox.Text, out var num)
                && num >= 1 && num <= 90)
            {
                foreach (var b in boards)
                    b.MarkNumber(num);

                BoardsDataGrid.Items.Refresh();
                CalledNumberTextBox.Clear();
            }
        }

        private void BoardsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (BoardsDataGrid.SelectedItem is BingoBoard board)
            {
                var win = new BoardWindow(board);
                win.Owner = this;
                win.Show();
            }
        }
    }
}
