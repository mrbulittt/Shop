using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Views;

namespace Shop.Pages;

public partial class OrderPage : UserControl
{
    public OrderPage()
    {
        InitializeComponent();

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IsOrder == true && x.IdUser == VariableData.authenticatedUser.IdUser).ToList();
        
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

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        var selectedOrder = DataGridItems.SelectedItem as Basket;
        if (selectedOrder == null) return;
        VariableData.selectedBasket = selectedOrder;
        
        var parent = this.VisualRoot as Window;
        var checkUsersOrder = new OrderDetailsInOrderList();
        await checkUsersOrder.ShowDialog(parent);
        
        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IsOrder == true && x.IdUser == VariableData.authenticatedUser.IdUser).ToList();
    }

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }
}