using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Shop.Data;

namespace Shop.Views;

public partial class CheckProdInformationForUsers : Window
{
    public CheckProdInformationForUsers()
    {
        InitializeComponent();
        DataContext = VariableData.selectedProduct;
        
        ComboCategory.ItemsSource = App.DbContext.ProdCategories.ToList();

        
        if (VariableData.selectedProduct == null)
        {
            DataContext = new Product();
        }

        if (VariableData.selectedProdCategory != null)
        {
            ComboCategory.SelectedItem = VariableData.selectedProduct.IdCategoryNavigation;
        }
        DataContext = VariableData.selectedProduct;
        
        
        
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