using eShop.UseCases.CustomerPortal.PluginInterfaces.StateStore;

namespace eShop.StateStore.DI;

public class StateStoreBase : IStateStore
{
    private Action? _listeners;

    public void AddStateChangeListeners(Action listener)
        => _listeners += listener;

    public void RemoveStateChangeListeners(Action listener)
        => _listeners -= listener;

    public void BroadCastStateChange()
    {
        if (_listeners != null)
            _listeners.Invoke();
    }
}
