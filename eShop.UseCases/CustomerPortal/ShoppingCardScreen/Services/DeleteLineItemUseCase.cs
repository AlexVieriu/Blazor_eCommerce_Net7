namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class DeleteLineItemUseCase : IDeleteLineItemUseCase
{
    private readonly IShoppingCart _cart;

    public DeleteLineItemUseCase(IShoppingCart cart)
    {
        _cart = cart;
    }

    public async Task<Order> ExecuteAsync(int productId)
    {
        return await _cart.DeleteProductFromCartAsync(productId);
    }
}
