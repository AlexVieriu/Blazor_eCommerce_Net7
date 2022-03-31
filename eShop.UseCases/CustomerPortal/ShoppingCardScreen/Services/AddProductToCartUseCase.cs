using eShop.UseCases.CustomerPortal.PluginInterfaces.UI;

namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class AddProductToCartUseCase : IAddProductToCartUseCase
{
    private readonly IShoppingCart _shoppingCart;

    public AddProductToCartUseCase(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }
    public async Task ExecuteAsync(Product product)
    {
        await _shoppingCart.AddProductToCartAsync(product);
    }
}
