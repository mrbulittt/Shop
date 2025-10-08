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
        
        if (VariableData.selectedOrder == null) return;

        DataContext = VariableData.selectedOrder;
    
        var orderItems = App.DbContext.Baskets
            .Where(b => b.IdOrder == VariableData.selectedOrder.IdOrder)
            .Join(App.DbContext.Products,
                b => b.IdProduct,
                p => p.IdProduct,
                (b, p) => new { Basket = b, Product = p })
            .Join(App.DbContext.ProdCategories,
                bp => bp.Product.IdCategory,
                c => c.IdCategory,
                (bp, c) => new
                {
                    bp.Product.NameProduct,
                    bp.Product.Price,
                    bp.Product.Description,
                    NameCategory = c.NameCategory,
                    bp.Basket.ProdCount,
                    bp.Basket.ResultPrice,
                    Total = bp.Basket.ResultPrice * bp.Basket.ProdCount
                })
            .ToList();
          

        DataGridItems.ItemsSource = orderItems;
        
    }
}