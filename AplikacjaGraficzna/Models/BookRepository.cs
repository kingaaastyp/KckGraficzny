using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AplikacjaGraficzna.Models
{
    public class BookRepository
    {
        private List<Book> books;
        private readonly string filePath;

        public BookRepository(string filePath)
        {
            this.filePath = filePath;
            books = LoadBooksFromFile();
        }

        private List<Book> LoadBooksFromFile()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<Book>();
                }

                string json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Book>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas ładowania pliku JSON: {ex.Message}");
                return new List<Book>();
            }
        }


        public void SaveBooksToFile()
        {
            // Zapis książek do pliku JSON
            try
            {
                string json = JsonSerializer.Serialize(books, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"Błąd podczas zapisu pliku: {ex.Message}");
            }
        }

        public List<Book> GetBooks()
        {
            // Pobranie pełnej listy książek
            return books;
        }

        public void AddBook(Book book)
        {
            // Sprawdzenie, czy książka już istnieje
            var existingBook = books.FirstOrDefault(b =>
                b.Title.Equals(book.Title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(book.Author, StringComparison.OrdinalIgnoreCase) &&
                b.Year == book.Year);

            if (existingBook != null)
            {
                // Komunikat o duplikacie
                throw new InvalidOperationException($"Książka \"{existingBook.Title}\" autorstwa {existingBook.Author} już istnieje.");
            }

            books.Add(book);
            SaveBooksToFile();
        }

        public void UpdateBook(Book oldBook, Book updatedBook)
        {
            // Aktualizacja istniejącej książki
            var index = books.IndexOf(oldBook);
            if (index != -1)
            {
                books[index] = updatedBook;
                SaveBooksToFile();
            }
        }

        public void RemoveBook(Book book)
        {
            // Usuwanie książki
            books.Remove(book);
            SaveBooksToFile();
        }

        public bool DoesBookExist(string title, string author, int year)
        {
            return books.Any(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(author, StringComparison.OrdinalIgnoreCase) &&
                b.Year == year);
        }


        public List<Book> SearchByTitle(string title)
        {
            // Wyszukiwanie książek po tytule
            return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> SearchByAuthor(string author)
        {
            // Wyszukiwanie książek po autorze
            return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Book> SortByTitle(bool ascending = true)
        {
            // Sortowanie książek według tytułu
            return ascending
                ? books.OrderBy(b => b.Title).ToList()
                : books.OrderByDescending(b => b.Title).ToList();
        }

        public List<Book> SortByAuthor(bool ascending = true)
        {
            // Sortowanie książek według autora
            return ascending
                ? books.OrderBy(b => b.Author).ToList()
                : books.OrderByDescending(b => b.Author).ToList();
        }

        public List<Book> SortBooksByTitle(List<Book> booksToSort, bool ascending = true)
        {
            // Sortowanie konkretnej listy książek według tytułu
            return ascending
                ? booksToSort.OrderBy(b => b.Title).ToList()
                : booksToSort.OrderByDescending(b => b.Title).ToList();
        }
    }
}
