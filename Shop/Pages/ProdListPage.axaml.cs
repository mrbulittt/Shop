using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Shop.Data;
using Shop.Views;

namespace Shop.Pages;

public partial class ProdListPage : UserControl
{
    private List<Product> allProducts = new List<Product>();
    public ProdListPage()
    {
        InitializeComponent();
        LoadData();
        
        if (VariableData.authenticatedUser.IdRole == 2)
        {
            EmployeeListBtn.IsVisible = false;
        }
        if (VariableData.authenticatedUser.IdRole == 3)
        {
            UsersListBtn.IsVisible = false;
            EmployeeListBtn.IsVisible = false;
            AddBtn.IsVisible = false;
            AllOrdersListBtn.IsVisible = false;
        }
    }

    private void LoadData()
    {
        allProducts = App.DbContext.Products
            .Include(p => p.IdCategoryNavigation)
            .ToList();
        
        ComboCategory.ItemsSource = App.DbContext.ProdCategories.ToList();
        DataGridItems.ItemsSource = allProducts;
    }


    private void ComboCategory_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private void ApplyFilter()
    {
        if (ComboCategory.SelectedItem is ProdCategory selectedCategory)
        {
            var filtered = allProducts
                .Where(p => p.IdCategoryNavigation?.IdCategory == selectedCategory.IdCategory) // Фильтр по ID надежнее
                .ToList();
            DataGridItems.ItemsSource = filtered;
        }
        else
        {
            DataGridItems.ItemsSource = allProducts;
        }
    }
    
    private void MinPrice_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private void MaxPrice_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        ApplyAllFilter();
    }

    private IEnumerable<Product> ApplyPriceFilter(IEnumerable<Product> products)
    {
        string minPriceText = MinPriceText.Text;
        string maxPriceText = MaxPriceText.Text;

        var filteredProducts = products.AsEnumerable();

        // Фильтр по минимальной цене
        if (!string.IsNullOrEmpty(minPriceText) && double.TryParse(minPriceText, out double minPrice))
        {
            filteredProducts = filteredProducts.Where(p => p.Price >= (decimal)minPrice);
        }

        // Фильтр по максимальной цене
        if (!string.IsNullOrEmpty(maxPriceText) && double.TryParse(maxPriceText, out double maxPrice))
        {
            filteredProducts = filteredProducts.Where(p => p.Price <= (decimal)maxPrice);
        }

        return filteredProducts;
        DataGridItems.ItemsSource = filteredProducts;
    }

    private void ApplyAllFilter()
    {
        var filteredProducts = allProducts.AsEnumerable();
    
        if (ComboCategory.SelectedItem is ProdCategory selectedCategory)
        {
            filteredProducts = filteredProducts
                .Where(p => p.IdCategoryNavigation != null &&
                            p.IdCategoryNavigation.IdCategory == selectedCategory.IdCategory);
        }

        filteredProducts = ApplyPriceFilter(filteredProducts);

        DataGridItems.ItemsSource = filteredProducts;
    }


    private void ResetButton_Click(object? sender, RoutedEventArgs e)
    {
        DataGridItems.ItemsSource = allProducts;
        ComboCategory.SelectedItem = null;
        MinPriceText.Text = string.Empty;
        MaxPriceText.Text = string.Empty;
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

    private async void DataGrid_DoubleTapped(object? sender, TappedEventArgs e)
    {
        if (VariableData.authenticatedUser.IdRole == 1 || VariableData.authenticatedUser.IdRole == 2)
        {

            var selectedProduct = DataGridItems.SelectedItem as Product;
            if (selectedProduct == null) return;

            VariableData.selectedProduct = selectedProduct;
            VariableData.selectedProdCategory = selectedProduct.IdCategoryNavigation;

            var parent = this.VisualRoot as Window;
            var changeProduct = new AddAndChangeProd();
            await changeProduct.ShowDialog(parent);

            DataGridItems.ItemsSource = App.DbContext.Products.ToList();

        }
        else
        {
            var selectedProduct = DataGridItems.SelectedItem as Product;
            if (selectedProduct == null) return;

            VariableData.selectedProduct = selectedProduct;
            VariableData.selectedProdCategory = selectedProduct.IdCategoryNavigation;

            var parent = this.VisualRoot as Window;
            var viewProd = new CheckProdInformationForUsers();
            await viewProd.ShowDialog(parent);

            DataGridItems.ItemsSource = App.DbContext.Products.ToList();
        }
    }

    private void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authenticatedUser.IdRole == 1 || VariableData.authenticatedUser.IdRole == 2)
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
        else
        {
            return;
        }
    }

    private async void AddButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VariableData.authenticatedUser.IdRole == 3)
        {
            IsVisible = false;
        }
        
        VariableData.selectedProduct = null;
        
        var parent = this.VisualRoot as Window;
        var changeProduct = new AddAndChangeProd();
        await changeProduct.ShowDialog(parent);
        
        LoadData();

    }

    private void BasketAdd_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Product product)
        {
            var existingBasketItem = App.DbContext.Baskets
                .FirstOrDefault(b => b.IdProduct == product.IdProduct && 
                                     b.IdUser == VariableData.authenticatedUser.IdUser);
        
            if (existingBasketItem != null)
            {
                existingBasketItem.ProdCount += 1;
                existingBasketItem.ResultPrice = product.Price * existingBasketItem.ProdCount;
            }
            else
            {
                var basketItem = new Basket()
                {
                    IdProduct = product.IdProduct,
                    ProdCount = 1,
                    IdUser = VariableData.authenticatedUser.IdUser,
                    ResultPrice = product.Price 
                };
                App.DbContext.Baskets.Add(basketItem);
            }
        
            App.DbContext.SaveChanges();
        }
    }

    

    private void UsersList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<UsersListPage>();
    }

    private void EmployeeList(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<EmployeeListPage>();
    }

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }


    
}