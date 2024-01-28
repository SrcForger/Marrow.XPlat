using Avalonia.Controls;
using Avalonia.Interactivity;
using Sample.ViewModels;

namespace Sample.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void TestClick1(object? sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();
        }
    }
}