using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public partial class BookListWindow : Window
    {
        private readonly List<Book> _books;

        public BookListWindow(List<Book> books)
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}