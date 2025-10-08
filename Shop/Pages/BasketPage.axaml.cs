using System;
using System.Data.Common;
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

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IdUser == VariableData.authenticatedUser.IdUser && x.IsOrder == false).ToList();
        
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

        DataGridItems.ItemsSource = App.DbContext.Baskets.Where(x => x.IdUser == VariableData.authenticatedUser.IdUser && x.IsOrder == false).ToList();
    }

    private async void Order_Click(object? sender, RoutedEventArgs e)
    {
        var userBaskets = App.DbContext.Baskets
            .Where(x => x.IdUser == VariableData.authenticatedUser.IdUser && x.IsOrder == false)
            .ToList();

        if (userBaskets != null && userBaskets.Any())
        {
            DateTime orderDate = DateTime.UtcNow;
        
            var newOrder = new Order
            {
                IdUser = VariableData.authenticatedUser.IdUser,
                OrderDate = orderDate,
                TotalAmount = userBaskets.Sum(x => x.ResultPrice * x.ProdCount),
                Status = "Оформлен"
            };
        
            App.DbContext.Orders.Add(newOrder);
            App.DbContext.SaveChanges(); 
        
            foreach (var basket in userBaskets)
            {
                basket.IsOrder = true;
                basket.IdOrder = newOrder.IdOrder; 
                App.DbContext.Baskets.Update(basket);
            }
        
            App.DbContext.SaveChanges();

            MessageBoxManager.GetMessageBoxStandard("ЗАКАЗ ОФОРМЛЕН", $"Заказ №{newOrder.IdOrder} оформлен успешно!\n" +
                                                        $"Дата: {orderDate:dd.MM.yyyy HH:mm}\n" +
                                                        $"Сумма: {newOrder.TotalAmount} руб.\n" +
                                                        $"Товаров: {userBaskets.Count} шт.").ShowAsync();
        }
        else
        {
            MessageBoxManager.GetMessageBoxStandard("ОШИБКА","Корзина пуста! Добавьте товары перед оформлением заказа.").ShowAsync();
        }

        DataGridItems.ItemsSource = App.DbContext.Baskets
            .Where(x => x.IdUser == VariableData.authenticatedUser.IdUser && x.IsOrder == false)
            .ToList();
    }

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }
}