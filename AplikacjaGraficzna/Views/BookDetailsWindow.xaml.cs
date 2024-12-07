using System.Windows;
using AplikacjaGraficzna.Models;

namespace AplikacjaGraficzna.Views
{
    public partial class BookDetailsWindow : Window
    {
        public BookDetailsWindow(Book book)
        {
            InitializeComponent();
            DataContext = book; // Ustaw książkę jako źródło danych dla okna
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zamknij okno
        }
    }
}