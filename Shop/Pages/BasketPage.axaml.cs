using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Shop.Pages;

public partial class BasketPage : UserControl
{
    public BasketPage()
    {
        InitializeComponent();
    }

    private void MainPage(object? sender, RoutedEventArgs e)
    { 
        NavigationService.NavigateTo<MainPage>();
    }

    private void ProductList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ProdListPage>();
    }

    private void OrderHistory(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<OrderPage>();
    }

    private void Basket(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<BasketPage>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AuthPage>();
    }

    private void UsersList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<UsersListPage>();
    }

    private void EmployeeList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<EmployeeListPage>();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}