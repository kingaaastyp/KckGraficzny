using System.Windows;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public partial class EditBookWindow : Window
    {
        public Book Book { get; private set; }

        public EditBookWindow(Book book)
        {
            InitializeComponent();
            Book = book;

            TitleTextBox.Text = book.Title;
            AuthorTextBox.Text = book.Author;
            GenreTextBox.Text = book.Genre;
            YearTextBox.Text = book.Year.ToString();
            PageCountTextBox.Text = book.PageCount.ToString();
            DescriptionTextBox.Text = book.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Book.Title = TitleTextBox.Text;
            Book.Author = AuthorTextBox.Text;
            Book.Genre = GenreTextBox.Text;
            Book.Year = int.Parse(YearTextBox.Text);
            Book.PageCount = int.Parse(PageCountTextBox.Text);
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