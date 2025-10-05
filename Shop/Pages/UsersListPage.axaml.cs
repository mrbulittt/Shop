using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Pages;

public partial class UsersListPage : UserControl
{
    public UsersListPage()
    {
        InitializeComponent();
        DataGridUsers.ItemsSource = App.DbContext.Users.Where(x => x.IdRole == 3).ToList();
        

    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        Console.WriteLine("Deleting!");

        var button = sender as Button;
        var selectedUser = button?.DataContext as User;
        

        Console.WriteLine((selectedUser == null) ? "User not found" : "User founded");

        if (selectedUser == null) return;

        VariableData.selectedUser = selectedUser;

        App.DbContext.Users.Remove(selectedUser);
        App.DbContext.SaveChanges();

        DataGridUsers.ItemsSource = App.DbContext.Users.ToList();

    }

    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
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
}