using eShop.UseCases.CustomerPortal.PluginInterfaces.StateStore;
using eShop.UseCases.CustomerPortal.PluginInterfaces.UI;

namespace eShop.StateStore.DI;
public class StateStoreShoppingCart : StateStoreBase,IShoppingCartStateStore
{
    private readonly IShoppingCart _cart;

    public StateStoreShoppingCart(IShoppingCart cart)
    {
        _cart = cart;
    }
    public async Task<int> GetItemsCount()
    {
        var order = await _cart.GetOrderAsync();
        if (order != null && order.LineItems != null && order.LineItems.Count > 0)
            return order.LineItems.Count;

        return 0;
    }    
}
