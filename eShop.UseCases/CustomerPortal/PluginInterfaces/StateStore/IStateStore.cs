namespace eShop.UseCases.CustomerPortal.PluginInterfaces.StateStore;
public interface IStateStore
{
    void AddStateChangeListeners(Action listener);
    void RemoveStateChangeListeners(Action listener);
    void BroadCastStateChange(Action listener);
}
