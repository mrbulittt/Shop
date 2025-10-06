using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using Shop.Data;
using Tmds.DBus.Protocol;

namespace Shop.Pages;

public partial class BasketPage : UserControl
{
    public BasketPage()
    {
        InitializeComponent();

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IdUser == VariableData.authenticatedUser.IdUser).ToList();
        
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

        if (selectedBasket != null)
        {
            if (selectedBasket.ProdCount > 1)
            {
                selectedBasket.ProdCount -= 1;
                selectedBasket.ResultPrice = selectedBasket.IdProductNavigation.Price * selectedBasket.ProdCount;
            }
            else
            {
                App.DbContext.Baskets.Remove(selectedBasket);
            }
        }
       
        
        App.DbContext.SaveChanges();

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IdUser == VariableData.authenticatedUser.IdUser).ToList();
    }

    private void Order_Click(object? sender, RoutedEventArgs e)
    {
        var userBasketItems = App.DbContext.Baskets
            .Where(b => b.IdUser == VariableData.authenticatedUser.IdUser && b.IdOrder == null)
            .Include(b => b.IdProductNavigation)
            .ToList();

        if (!userBasketItems.Any())
        {
            MessageBoxManager.GetMessageBoxStandard("Уведомление", "Корзина пуста");
            return;
        }

        
        var newOrder = new Order
        {
            IdUser = VariableData.authenticatedUser.IdUser,
            Status = "Оформлен",
            TotalAmount = (int)userBasketItems.Sum(b => b.ResultPrice ?? 0)
        };

        App.DbContext.Orders.Add(newOrder);
        App.DbContext.SaveChanges();
        

        MessageBoxManager.GetMessageBoxStandard("Уведомление",$"Заказ №{newOrder.IdOrder} оформлен успешно!");
    
        // Показываем только товары не в заказах (в корзине)
        DataGridItems.ItemsSource = App.DbContext.Baskets
            .Where(x => x.IdUser == VariableData.authenticatedUser.IdUser && x.IdOrder == null)
            .ToList();
    }

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }
}