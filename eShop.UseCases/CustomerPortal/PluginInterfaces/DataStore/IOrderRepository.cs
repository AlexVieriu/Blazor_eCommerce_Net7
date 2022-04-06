namespace eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
public interface IOrderRepository
{
    Task<int> CreateOrderAsync(Order order);
    Task<IEnumerable<OrderLineItem>> GetLineItemsByOrderIdAsync(int orderId);
    Task<Order> GetOrderAsync(int orderId);
    Task<Order> GetOrderByUniqueIdAsync(string uniquieId);
    Task<IEnumerable<Order>> GetOrdersAsync();
    Task<IEnumerable<Order>> GetOutStandingsOrdersAsync();
    Task<IEnumerable<Order>> GetProcessedOrdersAsync();
    Task UpdateOrderAsync(Order order);  
}
