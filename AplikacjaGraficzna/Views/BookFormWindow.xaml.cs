using System.Windows;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public partial class BookFormWindow : Window
    {
        public Book Book { get; private set; }

        public BookFormWindow(Book existingBook = null)
        {
            InitializeComponent();

            if (existingBook != null)
            {
                TitleTextBox.Text = existingBook.Title;
                AuthorTextBox.Text = existingBook.Author;
                GenreTextBox.Text = existingBook.Genre;
                YearTextBox.Text = existingBook.Year.ToString();
                PageCountTextBox.Text = existingBook.PageCount.ToString();
                DescriptionTextBox.Text = existingBook.Description;

                Book = existingBook;
            }
            else
            {
                Book = new Book();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
                string.IsNullOrWhiteSpace(AuthorTextBox.Text) ||
                string.IsNullOrWhiteSpace(GenreTextBox.Text) ||
                !int.TryParse(YearTextBox.Text, out int year) ||
                !int.TryParse(PageCountTextBox.Text, out int pageCount))
            {
                MessageBox.Show("Proszę poprawnie wypełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Book.Title = TitleTextBox.Text;
            Book.Author = AuthorTextBox.Text;
            Book.Genre = GenreTextBox.Text;
            Book.Year = year;
            Book.PageCount = pageCount;
            Book.Description = DescriptionTextBox.Text;

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}