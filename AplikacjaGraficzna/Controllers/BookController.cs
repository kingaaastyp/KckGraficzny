using System.Collections.Generic;
using AplikacjaGraficzna.Models;
using AplikacjaGraficzna.Views;

namespace AplikacjaGraficzna.Controllers
{
    public class BookController
    {
        private readonly BookRepository repository;
        private readonly WpfBookView view;

        public BookController(BookRepository repository, WpfBookView view)
        {
            this.repository = repository;
            this.view = view;
        }

        public List<Book> GetBooks()
        {
            return repository.GetBooks();
        }

        public void DisplayAllBooks()
        {
            // Pobieranie i wyświetlanie wszystkich książek
            List<Book> books = repository.GetBooks();
            view.DisplayBooks(books);
        }

        public void SearchBookByTitle(string title)
        {
            // Szukanie książki po tytule
            List<Book> books = repository.SearchByTitle(title);
            if (books.Count == 0)
            {
                view.DisplayMessage("Nie znaleziono książek o podanym tytule.");
                return;
            }

            view.DisplayBooks(books);
        }

        public void SearchBookByAuthor(string author)
        {
            // Szukanie książek po autorze
            List<Book> books = repository.SearchByAuthor(author);
            if (books.Count == 0)
            {
                view.DisplayMessage("Nie znaleziono książek tego autora.");
                return;
            }

            view.DisplayBooks(books);
        }

        public void AddBook()
        {
            Book newBook = view.GetBookDetails();
            if (newBook == null)
            {
                view.DisplayMessage("Dodawanie książki zostało anulowane.");
                return;
            }

            repository.AddBook(newBook);
            repository.SaveBooksToFile();
            view.DisplayMessage($"Książka \"{newBook.Title}\" została dodana i zapisana.");
        }
        public void EditBook(Book bookToEdit)
        {
            if (bookToEdit == null)
            {
                view.DisplayMessage("Nie wybrano książki do edycji.");
                return;
            }

            // Pobranie zaktualizowanych szczegółów książki
            Book updatedBook = view.GetBookDetails(bookToEdit);
            if (updatedBook == null)
            {
                view.DisplayMessage("Edycja książki została anulowana.");
                return;
            }

            repository.UpdateBook(bookToEdit, updatedBook);
            view.DisplayMessage($"Książka \"{updatedBook.Title}\" została zaktualizowana.");
        }



/*
        public void EditBook(string title, string author)
        {
            // Edycja książki
            Book bookToEdit = repository.GetBooks()
                .Find(b => b.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase) &&
                           b.Author.Equals(author, System.StringComparison.OrdinalIgnoreCase));

            if (bookToEdit == null)
            {
                view.DisplayMessage("Nie znaleziono książki o podanych danych.");
                return;
            }

            Book updatedBook = view.GetBookDetails();
            if (updatedBook == null)
            {
                view.DisplayMessage("Edycja książki została anulowana.");
                return;
            }

            repository.UpdateBook(bookToEdit, updatedBook);
            view.DisplayMessage($"Książka \"{updatedBook.Title}\" została zaktualizowana.");
        }*/
        public Book SelectBook(List<Book> books)
        {
            return view.SelectBook(books); // Wywołanie metody widoku do wyboru książki
        }


        public void DeleteBook(string title, string author)
        {
            // Usuwanie książki
            Book bookToRemove = repository.GetBooks()
                .Find(b => b.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase) &&
                           b.Author.Equals(author, System.StringComparison.OrdinalIgnoreCase));

            if (bookToRemove == null)
            {
                view.DisplayMessage("Nie znaleziono książki o podanych danych.");
                return;
            }

            repository.RemoveBook(bookToRemove);
            view.DisplayMessage($"Książka \"{bookToRemove.Title}\" została usunięta.");
        }
    }
}
