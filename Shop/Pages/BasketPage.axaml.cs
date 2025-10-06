using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Pages;

public partial class BasketPage : UserControl
{
    public BasketPage()
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
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedBasket= button?.DataContext as Basket;
        

        Console.WriteLine((selectedBasket == null) ? "Item not found" : "Item founded");

        if (selectedBasket == null) return;

        VariableData.selectedBasket = selectedBasket;

        App.DbContext.Baskets.Remove(selectedBasket);
        App.DbContext.SaveChanges();

        DataGridItems.ItemsSource = App.DbContext.Baskets.ToList();
    }
}