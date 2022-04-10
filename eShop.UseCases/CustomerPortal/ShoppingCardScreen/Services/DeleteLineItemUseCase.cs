namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class DeleteLineItemUseCase : IDeleteLineItemUseCase
{
    private readonly IShoppingCart _cart;
    private readonly IShoppingCartStateStore _stateStore;

    public DeleteLineItemUseCase(IShoppingCart cart, IShoppingCartStateStore stateStore)
    {
        _cart = cart;
        _stateStore = stateStore;
    }

    public async Task<Order> ExecuteAsync(int productId)
    {
        var order = await _cart.DeleteProductFromCartAsync(productId);
        _stateStore.BroadCastStateChange();

        return order;
    }
}
