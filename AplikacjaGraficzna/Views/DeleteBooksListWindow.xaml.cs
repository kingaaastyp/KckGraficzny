using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public partial class DeleteBooksListWindow : Window
    {
        private List<Book> _books;

        public DeleteBooksListWindow(List<Book> books)
        {
            InitializeComponent();
            _books = books;
            BooksDataGrid.ItemsSource = _books;
        }

        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            var sortCriteria = (SortCriteriaComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var sortOrder = (SortOrderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (sortCriteria == "Tytuł")
            {
                BooksDataGrid.ItemsSource = sortOrder == "Rosnąco"
                    ? _books.OrderBy(b => b.Title).ToList()
                    : _books.OrderByDescending(b => b.Title).ToList();
            }
            else if (sortCriteria == "Autor")
            {
                BooksDataGrid.ItemsSource = sortOrder == "Rosnąco"
                    ? _books.OrderBy(b => b.Author).ToList()
                    : _books.OrderByDescending(b => b.Author).ToList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = (Book)BooksDataGrid.SelectedItem;

            if (selectedBook == null)
            {
                MessageBox.Show("Proszę wybrać książkę do usunięcia.", "Brak wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Wyświetl potwierdzenie
            var result = MessageBox.Show(
                $"Czy na pewno chcesz usunąć książkę:\n\n" +
                $"Tytuł: {selectedBook.Title}\n" +
                $"Autor: {selectedBook.Author}\n" +
                $"Rok wydania: {selectedBook.Year}\n\n" +
                "Operacji nie można cofnąć.",
                "Potwierdzenie usunięcia",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _books.Remove(selectedBook);
                BooksDataGrid.Items.Refresh();
                MessageBox.Show("Książka została usunięta.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true; // Sygnał, że książka została usunięta
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
