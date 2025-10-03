using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop
{
    public partial class App : Application
    {
        public static AppDbContext DbContext { get; private set; } = new AppDbContext();
        public override void Initialize()
        {
            
            DbContext.Roles.ToList();
            DbContext.Users.ToList();
            DbContext.Products.ToList();
            DbContext.Orders.ToList();
            DbContext.Baskets.ToList();
            DbContext.ProdCategories.ToList();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}