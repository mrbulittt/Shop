using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Pages;

public partial class ProdListPage : UserControl
{
    public ProdListPage()
    {
        InitializeComponent();
        DataGridItems.ItemsSource = App.DbContext.Products.ToList();
        
        if (VariableData.authenticatedUser.IdRole == 2)
        {
            EmployeeListBtn.IsVisible = false;
        }
        if (VariableData.authenticatedUser.IdRole == 3)
        {
            UsersListBtn.IsVisible = false;
            EmployeeListBtn.IsVisible = false;
            AddBtn.IsVisible = false;
            
        }
    }

    private void MainPage(object? sender, RoutedEventArgs e)
    { 
        NavigationService.NavigateTo<MainPage>();
    }

    private void Basket(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<BasketPage>();
    }

    private void OrderHistory(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<OrderPage>();
    }

    private void ProdList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<ProdListPage>();
    }

    private void Leave(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AuthPage>();
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedProduct = button?.DataContext as Product;
        

        Console.WriteLine((selectedProduct == null) ? "Item not found" : "Item founded");

        if (selectedProduct == null) return;

        VariableData.selectedProduct = selectedProduct;

        App.DbContext.Products.Remove(selectedProduct);
        App.DbContext.SaveChanges();

        DataGridItems.ItemsSource = App.DbContext.Products.ToList();
    }

    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authenticatedUser.IdUser == 3)
        {
            IsVisible = false;
        }
    }

    private void BasketAdd_Click(object? sender, RoutedEventArgs e)
    {
        
    }

    private void BasketDelete_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void UsersList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<UsersListPage>();
    }

    private void EmployeeList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<EmployeeListPage>();
    }
}