using eShop.CoreBusiness.Models;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;

namespace eShop.DataStore;
public class OrderRepository : IOrderRepository
{
    private Dictionary<int, Order> Orders;
    public OrderRepository()
    {
        Orders = new();
    }

    public async Task<int> CreateOrderAsync(Order order)
    {
        order.OrderId = Orders.Count + 1;
        Orders.Add(order.OrderId.Value, order);
        return order.OrderId.Value;
    }

    public async Task<IEnumerable<OrderLineItem>> GetLineItemsByOrderIdAsync(int orderId)
    {
        var lineItems = Orders.Values.Where(q => q.OrderId == orderId)
                                     .Select(x => x.LineItems).FirstOrDefault().AsEnumerable();

        return lineItems;
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        return Orders[orderId];
    }

    public async Task<Order> GetOrderByUniqueIdAsync(string uniquieId)
    {
        var order = Orders.Values.FirstOrDefault(q => q.UniqueId == uniquieId);
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        return Orders.Values;
    }

    public async Task<IEnumerable<Order>> GetOutStandingsOrdersAsync()
    {
        return Orders.Values.Where(q => q.DateProcessed.HasValue == false);
    }

    public async Task<IEnumerable<Order>> GetProcessedOrdersAsync()
    {
        return Orders.Values.Where(q => q.DateProcessed.HasValue == true);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        if (order != null && order.LineItems != null && order.LineItems.Count > 0)
            Orders[order.OrderId.Value] = order;

        return;
    }
}
