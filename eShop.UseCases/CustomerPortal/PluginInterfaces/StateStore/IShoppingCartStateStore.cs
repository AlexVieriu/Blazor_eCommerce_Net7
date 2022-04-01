namespace eShop.UseCases.CustomerPortal.PluginInterfaces.StateStore;
public interface IShoppingCartStateStore
{
    Task<int> GetItemsCount();
}
