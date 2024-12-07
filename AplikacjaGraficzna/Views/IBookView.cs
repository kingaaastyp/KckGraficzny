using System.Collections.Generic;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public interface IBookView
    {
        void DisplayMessage(string message); // Wyświetlanie komunikatów
        void DisplayError(string message); // Wyświetlanie błędów
        void DisplayBooks(List<Book> books); // Wyświetlanie listy książek
        Book GetBookDetails(Book existingBook = null); // Pobieranie szczegółów książki
        Book SelectBook(List<Book> books); // Wybór książki z listy
    }
}