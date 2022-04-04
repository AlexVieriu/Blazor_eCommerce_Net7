namespace eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
public interface IOrderRepository
{
    int CreateOrder(Order order);
    Order GetOrder(int orderId);
    Order GetOrderByUniqueId(string uniquieId);
    IEnumerable<Order> GetOrders();
    IEnumerable<Order> GetOutStandingsOrders();
    IEnumerable<Order> GetProcessedOrders();
    void UpdateOrder(Order order);  
}
