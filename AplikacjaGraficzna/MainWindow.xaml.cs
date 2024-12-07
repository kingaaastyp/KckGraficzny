using System.Windows;
using AplikacjaGraficzna.Controllers;
using AplikacjaGraficzna.Helpers;
using AplikacjaGraficzna.Models;
using AplikacjaGraficzna.Views;

namespace AplikacjaGraficzna
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookController _controller;

        public MainWindow()
        {
            InitializeComponent();
            string filePath = @"books.json"; // Ścieżka do pliku JSON
            var repository = new BookRepository(filePath);
            var view = new WpfBookView(repository); // Przekazujemy `repository` do `WpfBookView`
            _controller = new BookController(repository, view);
        }

        private void ShowBooks_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetl listę książek w trybie przeglądania
            var bookListWindow = new BookListWindow(_controller.GetBooks());
            bookListWindow.ShowDialog();
        }

        private void SearchByTitle_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetlenie InputBox dla tytułu
            string title = InputBox.Show("Podaj tytuł książki:");
            if (!string.IsNullOrEmpty(title))
            {
                _controller.SearchBookByTitle(title);
            }
            else
            {
                MessageBox.Show("Tytuł książki nie może być pusty!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchByAuthor_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetlenie InputBox dla autora
            string author = InputBox.Show("Podaj autora książki:");
            if (!string.IsNullOrEmpty(author))
            {
                _controller.SearchBookByAuthor(author);
            }
            else
            {
                MessageBox.Show("Autor książki nie może być pusty!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Wywołanie metody dodawania książki
            _controller.AddBook();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz okno edycji listy książek
            var editBooksListWindow = new EditBooksListWindow(_controller.GetBooks());
            if (editBooksListWindow.ShowDialog() == true)
            {
                // Jeśli dokonano edycji, odśwież widok
                _controller.DisplayAllBooks();
            }
        }
        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            // Otwórz okno wyboru książki do usunięcia
            var deleteBooksListWindow = new DeleteBooksListWindow(_controller.GetBooks());
            deleteBooksListWindow.ShowDialog();

            // Odśwież listę książek po zamknięciu okna
            _controller.DisplayAllBooks();
        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            // Zamykanie aplikacji
            Application.Current.Shutdown();
        }
    }
}
