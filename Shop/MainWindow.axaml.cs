using Avalonia.Controls;
using Shop.Pages;

namespace Shop
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigationService.Initialize(MainControl);
            NavigationService.NavigateTo<AuthPage>();
        }
    }
}