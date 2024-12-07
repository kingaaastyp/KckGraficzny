using System.Collections.Generic;
using System.Windows;
using AplikacjaGraficzna.Models;
using AplikacjaGraficzna.Views;

namespace AplikacjaGraficzna
{
    public class WpfBookView : IBookView
    {
        private readonly BookRepository _repository;

        public WpfBookView(BookRepository repository)
        {
            _repository = repository;
        }

        public void DisplayMessage(string message)
        {
            MessageBox.Show(message, "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void DisplayBooks(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                DisplayMessage("Brak książek do wyświetlenia.");
                return;
            }

            var bookListWindow = new BookListWindow(books);
            bookListWindow.ShowDialog();
        }

        public void DisplayDetails(Book book)
        {
            if (book == null)
            {
                DisplayMessage("Nie wybrano książki.");
                return;
            }

            var detailsWindow = new BookDetailsWindow(book);
            detailsWindow.ShowDialog();
        }

        public Book GetBookDetails(Book existingBook = null)
        {
            // Use BookFormWindow for creating or editing books
            var bookForm = new BookFormWindow(existingBook);
            if (bookForm.ShowDialog() == true)
            {
                return bookForm.Book; // Return the new or updated book
            }
            return null; // Return null if the operation was cancelled
        }

        public Book SelectBook(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                DisplayMessage("Brak książek do wyboru.");
                return null;
            }

            var editBooksListWindow = new EditBooksListWindow(books);
            if (editBooksListWindow.ShowDialog() == true)
            {
                return editBooksListWindow.SelectedBook; // Return the selected book
            }
            return null;
        }

        public void EditBook(Book bookToEdit)
        {
            if (bookToEdit == null)
            {
                DisplayMessage("Nie wybrano książki do edycji.");
                return;
            }

            var editWindow = new EditBookWindow(bookToEdit);
            if (editWindow.ShowDialog() == true)
            {
                DisplayMessage("Zmiany w książce zostały zapisane.");
            }
        }
    }
}
