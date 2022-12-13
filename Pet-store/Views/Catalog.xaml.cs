using Pet_store.Data;
using System.Windows;

namespace Pet_store.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        public Catalog() => InitializeComponent();

        private void Window_Loaded(object sender, RoutedEventArgs e) => SessionData.CurrentWindow = this;
    }
}
