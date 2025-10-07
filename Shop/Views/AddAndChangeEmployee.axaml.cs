using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Views;

public partial class AddAndChangeEmployee : Window
{
    public AddAndChangeEmployee()
    {
        InitializeComponent();
        if (VariableData.selectedUser == null)
        {
            DataContext = new User();
            return;
        }

        DataContext = VariableData.selectedUser;        
        
    }

    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameLoginText.Text) || string.IsNullOrWhiteSpace(FullNameText.Text) || 
            string.IsNullOrWhiteSpace(PasswordText.Text) || string.IsNullOrWhiteSpace(EmailText.Text) || string.IsNullOrWhiteSpace(AddressText.Text)
            || string.IsNullOrWhiteSpace(PhoneNumText.Text))
        {
            return;
        }

        var thisUser = DataContext as User;
        thisUser.IdRole = 2;

        if (VariableData.selectedUser == null)
        {
            App.DbContext.Users.Add(thisUser);
        }
        else
        {
            App.DbContext.Update(thisUser);
        }
        
        
        App.DbContext.SaveChanges();
        this.Close();
    }
}