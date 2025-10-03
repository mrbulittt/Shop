using Avalonia.Controls;
using Shop.Pages;

namespace Shop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainControl.Content = new AuthPage();
        }
    }
}