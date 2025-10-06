using System.Linq;
using Shop.Data;

namespace Shop;

public class VariableData
{
    
    public static User authenticatedUser { get; set; } 
    public static Basket authUserBasket { get; set; }
    public static Order authUserOrder { get; set; }
    public static User selectedUser {get;set;}
    public static Basket selectedBasket {get;set;}
    public static Product selectedProduct {get;set;}
    public static Order selectedOrder {get;set;}
    public static Role selectedRole {get;set;}
    public static ProdCategory selectedProdCategory {get;set;}
}