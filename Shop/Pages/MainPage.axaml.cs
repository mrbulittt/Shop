using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using Shop.Data;
using Shop.Views;
using Shop.Pages;


namespace Shop.Pages;

public partial class MainPage : UserControl
{
    User us;
    public MainPage()
    {
        InitializeComponent();
    }


    private void SaveButton(object? sender, RoutedEventArgs e)
    {
    }

    private void ProductList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ProdListPage>();
    }

    private void Basket(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<BasketPage>();
    }

    private void OrderHistory(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<OrderPage>();
    }
}