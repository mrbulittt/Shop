using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Pages;

public partial class OrderPage : UserControl
{
    public OrderPage()
    {
        InitializeComponent();
        
        if (VariableData.authenticatedUser.IdRole == 2)
        {
            EmployeeListBtn.IsVisible = false;
        }
        if (VariableData.authenticatedUser.IdRole == 3)
        {
            UsersListBtn.IsVisible = false;
            EmployeeListBtn.IsVisible = false;
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

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedOrder = button?.DataContext as Order;
        

        Console.WriteLine((selectedOrder == null) ? "Item not found" : "Item founded");

        if (selectedOrder == null) return;

        VariableData.selectedOrder = selectedOrder;

        App.DbContext.Orders.Remove(selectedOrder);
        App.DbContext.SaveChanges();

        DataGridItems.ItemsSource = App.DbContext.Orders.ToList();
    }
}