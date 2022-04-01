namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class UpdateQuantityUseCase : IUpdateQuantityUseCase
{
    private readonly IShoppingCart _cart;

    public UpdateQuantityUseCase(IShoppingCart cart)
    {
        _cart = cart;
    }

    public async Task<Order> ExecuteAsync(int productId, int qty)
    {
        return await _cart.UpdateQuantityAsync(productId, qty);
    }
}
