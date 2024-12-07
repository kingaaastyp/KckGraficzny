using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AplikacjaGraficzna.Helpers
{
    public static class InputBox
    {
        public static string Show(string prompt)
        {
            // Tworzenie okna dialogowego
            var window = new Window
            {
                Title = "Wprowadź dane",
                MinHeight = 200,
                MinWidth = 400,
                MaxHeight = 300,
                MaxWidth = 600,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.CanResize,
                Background = new SolidColorBrush(Color.FromRgb(243, 244, 246))
            };

            // Siatka główna
            var grid = new Grid
            {
                Margin = new Thickness(20)
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Tekst nagłówka
            var promptText = new TextBlock
            {
                Text = prompt,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Color.FromRgb(30, 58, 138)),
                Margin = new Thickness(0, 0, 0, 15),
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };
            grid.Children.Add(promptText);
            Grid.SetRow(promptText, 0);

            // Pole tekstowe
            var textBox = new TextBox
            {
                FontSize = 16,
                Padding = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 15),
                Width = 300
            };
            grid.Children.Add(textBox);
            Grid.SetRow(textBox, 1);

            // Przycisk OK
            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            var okButton = new Button
            {
                Content = "OK",
                Width = 100,
                Height = 40,
                Background = new SolidColorBrush(Color.FromRgb(37, 99, 235)),
                Foreground = Brushes.White,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5),
                BorderThickness = new Thickness(0)
            };
            okButton.Click += (sender, args) => { window.DialogResult = true; };

            buttonPanel.Children.Add(okButton);
            grid.Children.Add(buttonPanel);
            Grid.SetRow(buttonPanel, 2);

            // Ustawienie zawartości okna
            window.Content = grid;

            // Wyświetlenie okna dialogowego
            if (window.ShowDialog() == true)
            {
                return textBox.Text;
            }

            return null;
        }
    }
}
