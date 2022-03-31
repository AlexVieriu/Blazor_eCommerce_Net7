namespace eShop.UseCases.CustomerPortal.PluginInterfaces.UI;
public interface IShoppingCart
{    
    Task<Order> GetOrderAsync();   
    Task AddProductToCartAsync(Product product);
    Task<Order> DeleteProductFromCartAsync(int productId);
    Task EmptyAsync();
    Task UpdateOrderAsync(Order order);
    Task<Order> UpdateQuantityAsync(int productId, int quantity);
    Task SetOrderAsync(Order order);

}
