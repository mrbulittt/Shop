using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Shop.Data;

namespace Shop.Views;

public partial class OrderDetailsInOrderList : Window
{
    public OrderDetailsInOrderList()
    {
        InitializeComponent();

        if (VariableData.selectedBasket == null)
        {
            DataContext = new Basket();
            return;
        }
        DataContext = VariableData.selectedBasket;
    }
}