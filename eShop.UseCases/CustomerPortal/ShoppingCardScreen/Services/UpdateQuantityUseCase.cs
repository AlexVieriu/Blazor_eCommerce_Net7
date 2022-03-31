namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class UpdateQuantityUseCase : IUpdateQuantityUseCase
{
    private readonly IShoppingCart _shoppingCart;

    public UpdateQuantityUseCase(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public async Task<Order> ExecuteAsync(int productId, int qty)
    {
        return await _shoppingCart.UpdateQuantityAsync(productId, qty);
    }
}
