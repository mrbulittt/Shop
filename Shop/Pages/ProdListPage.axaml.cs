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
    }

    private void AddButton_Click(object? sender, RoutedEventArgs e)
    {
    }

    private void BasketAdd_Click(object? sender, RoutedEventArgs e)
    {
        var thisProd = DataContext as Product;
        
        var d = VariableData.selectedProduct.IdProduct;

        if (VariableData.selectedProduct == null)
        {
            
        }
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