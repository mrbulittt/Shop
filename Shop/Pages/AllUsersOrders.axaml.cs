using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Views;

namespace Shop.Pages;

public partial class AllUsersOrders : UserControl
{
    public AllUsersOrders()
    {
        InitializeComponent();

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IsOrder == true).ToList();
        
        if (VariableData.authenticatedUser.IdRole == 2)
        {
            EmployeeListBtn.IsVisible = false;
        }
        if (VariableData.authenticatedUser.IdRole == 3)
        {
            UsersListBtn.IsVisible = false;
            EmployeeListBtn.IsVisible = false;
            AllOrdersListBtn.IsVisible = false;
        }
    }

    private void MainPage(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPage>();
    }

    private void ProdList(object? sender, RoutedEventArgs e)
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

    private void UsersList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<UsersListPage>();
    }

    private void EmployeeList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<EmployeeListPage>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AuthPage>();
    }

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        var checkUsersOrder = new OrderDetailsInAllUsers();
        await checkUsersOrder.ShowDialog(parent);
    }

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }
}