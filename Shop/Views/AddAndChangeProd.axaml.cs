using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Shop.Data;

namespace Shop.Views;

public partial class AddAndChangeProd : Window
{
    public AddAndChangeProd()
    {
        InitializeComponent();
        DataContext = VariableData.selectedProduct;
        
        ComboCategory.ItemsSource = App.DbContext.ProdCategories.ToList();

        ComboCategory.SelectionChanged += ComboCategory_OnSelectionChanged;
        if (VariableData.selectedProduct == null)
        {
            DataContext = new Product();
            return;
        }

        if (VariableData.selectedProdCategory != null)
        {
            ComboCategory.SelectedItem = VariableData.selectedProduct.IdCategoryNavigation;
            return;
        }
        DataContext = VariableData.selectedProduct;
        
        
        UpdateImg();
    }

    private void SaveButton(object? sender, RoutedEventArgs e)
    {
        var selectedCategory = ComboCategory.SelectedItem as ProdCategory;
        
        if(string.IsNullOrEmpty(NameProdText.Text) || string.IsNullOrEmpty(PriceText.Text) || ComboCategory.SelectedItem == null) return;
        
        var prodDataContext = DataContext as Product;
        prodDataContext.IdCategory = selectedCategory.IdCategory;
        if (VariableData.selectedProduct == null)
        {
            App.DbContext.Products.Add(prodDataContext);
            
        }
        else
        {
            App.DbContext.Update(prodDataContext);
        }
        
        App.DbContext.SaveChanges();
        UpdateImg();
        this.Close();
    }

    private void ComboCategory_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        UpdateImg();
    }

    private void UpdateImg()
    {
        if (ComboCategory.SelectedItem is ProdCategory category)
        {
            var imagePath = category.NameCategory switch
            {
                "Хлебобулочные изделия" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\BreadProd.png",
                "Молочная продукция" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\MilkProd.png",
                "Закуски" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\SnackProd.png",
                "Энергетики" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\EnergyDrinkProd.png",
                "Мясное" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\MeatProd.png",
                "Табачная продукция" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\TabaccoProd.png",
                "Алкогольная продукция" => "C:\\Users\\bulat\\source\\repos\\Shop\\Shop\\images\\AlcoholProd.png"
            };
            
            ProductImage.Source = new Bitmap(imagePath) ;
        }
    }
}