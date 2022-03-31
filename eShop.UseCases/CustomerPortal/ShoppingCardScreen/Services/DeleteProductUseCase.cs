namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class DeleteProductUseCase : IDeleteProductUseCase
{
    private readonly IShoppingCart _shoppingCart;

    public DeleteProductUseCase(IShoppingCart shoppingCart)
    {
        _shoppingCart = shoppingCart;
    }

    public async Task<Order> ExecuteAsync(int productId)
    {
        return await _shoppingCart.DeleteProductFromCartAsync(productId);
    }
}
