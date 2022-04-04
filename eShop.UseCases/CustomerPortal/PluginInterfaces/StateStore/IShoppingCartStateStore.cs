namespace eShop.UseCases.CustomerPortal.PluginInterfaces.StateStore;
public interface IShoppingCartStateStore : IStateStore
{
    Task<int> GetItemsCount();
}
