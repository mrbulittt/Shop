using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using Shop.Data;
using Shop.Views;
using Shop.Pages;


namespace Shop.Pages;

public partial class MainPage : UserControl
{
    public MainPage()
    {
        InitializeComponent();
        
        if (VariableData.authenticatedUser == null)
        {
            DataContext = new User();
        }

        DataContext = VariableData.authenticatedUser;

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


    private async void SaveButton(object? sender, RoutedEventArgs e)
    {
        if(string.IsNullOrEmpty(FullNameText.Text) || string.IsNullOrEmpty(NameLoginText.Text) || string.IsNullOrEmpty(PasswordText.Text)
           || string.IsNullOrEmpty(EmailText.Text) || string.IsNullOrEmpty(AddressText.Text) 
           || string.IsNullOrEmpty(PhoneNumText.Text)) return;

        var thisUser = DataContext as User;
        

        if (VariableData.authenticatedUser != null)
        {
            App.DbContext.Users.Update(thisUser);
        }
        
        App.DbContext.SaveChanges();
        
    }

    private void ProductList(object? sender, RoutedEventArgs e)
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

    private void Main(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<MainPage>();
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

    private void AllOrdersListBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService.NavigateTo<AllUsersOrders>();
    }
}