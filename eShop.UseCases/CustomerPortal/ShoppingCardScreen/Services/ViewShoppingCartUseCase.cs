namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class ViewShoppingCartUseCase : IViewShoppingCartUseCase
{
    private readonly IShoppingCart _cart;

    public ViewShoppingCartUseCase(IShoppingCart cart)
    {
        _cart = cart;
    }

    public async Task<Order> ExecuteAsync()
    {
        return await _cart.GetOrderAsync();
    }
}
