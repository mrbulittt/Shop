using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MsBox.Avalonia;
using Shop.Data;
using Shop.Views;

namespace Shop.Pages;

public partial class AuthPage : UserControl
{
    private MainWindow _mainWindow;
    public AuthPage()
    {
        InitializeComponent();
        _mainWindow = (MainWindow)VisualRoot;
    }

    private async void LoginBtn_Click(object? sender, RoutedEventArgs e)
    {
        string login = LoginText.Text;
        string password = PasswordText.Text;
        var d = App.DbContext.Logins.FirstOrDefault(x => x.NameLogin == login && x.Password == password);
        if (d != null)
        {
            await MessageBoxManager.GetMessageBoxStandard("Успех", "Добро пожаловать").ShowAsync();
            _mainWindow.MainControl.Content = new MainPage(d);
                        
        }
        else
        {
            await MessageBoxManager.GetMessageBoxStandard("Провал",
                "Такого пользователя не существует. Перепроверьте данные или зарегайтесь!").ShowAsync();
        }
    }

    private void RegisterButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var parent = this.VisualRoot as Window;
        var registerwindow = new RegisterNewUser();
        await registerwindow.ShowDialog(parent);
    }
}