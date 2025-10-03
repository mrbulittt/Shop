using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Views;

public partial class RegisterNewUser : Window
{
    public RegisterNewUser()
    {
        InitializeComponent();

        if (VariableData.selectedUser != null)
        {
            var user = App.DbContext.Users.FirstOrDefault(x => x.IdUser == VariableData.selectedUser.IdUser);

            if (user != null)
            {
                DataContext = user;
            }
        }

        else
        {
            DataContext = new User();

        }
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

        if (thisUser == null) return;

        if (VariableData.selectedUser != null)
        {

            thisUser.Login = NameLoginText.Text;
            thisUser.Password = PasswordText.Text;

            

            App.DbContext.Update(thisUser);
            if (thisUser != null)
            {
                App.DbContext.Update(thisUser);
            }
        }
        else
        {
            if (thisUser == null)
            {
                thisUser = new User();
            }


            App.DbContext.Users.Add(thisUser);
        }

        App.DbContext.SaveChanges();
        this.Close();
    }
}