using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;
using Shop.Views;

namespace Shop.Pages;

public partial class EmployeeListPage : UserControl
{
    public EmployeeListPage()
    {
        InitializeComponent();
        DataGridUsers.ItemsSource = App.DbContext.Users.Where(x => x.IdRole == 2).ToList();
        
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

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        VariableData.selectedUser = null;
        
        var parent = this.VisualRoot as Window;
        var addwinwEmployee = new AddAndChangeEmployee();
        await addwinwEmployee.ShowDialog(parent);
        
        DataGridUsers.ItemsSource = App.DbContext.Users.Where(x => x.IdRole == 2).ToList();
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
        var selectedUser = DataGridUsers.SelectedItem as User;
        if(selectedUser == null)return;
        
        VariableData.selectedUser = selectedUser;
        
        var parent = this.VisualRoot as Window;
        var addwinwEmployee = new AddAndChangeEmployee();
        await addwinwEmployee.ShowDialog(parent);
        
        
        DataGridUsers.ItemsSource = App.DbContext.Users.Where(x => x.IdRole == 2).ToList();
    }
}