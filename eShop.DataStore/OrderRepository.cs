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

    public int CreateOrder(Order order)
    {
        order.OrderId = Orders.Count + 1;
        Orders.Add(order.OrderId.Value, order);
        return order.OrderId.Value;
    }

    public Order GetOrder(int orderId)
    {
        return Orders[orderId];
    }

    public Order GetOrderByUniqueId(string uniquieId)
    {
        var order = Orders.Values.FirstOrDefault(q => q.UniqueId == uniquieId);
        return order;
    }

    public IEnumerable<Order> GetOrders()
    {
        return Orders.Values;
    }

    public IEnumerable<Order> GetOutStandingsOrders()
    {
        return Orders.Values.Where(q => q.DateProcessed.HasValue == true);
    }

    public IEnumerable<Order> GetProcessedOrders()
    {
        return Orders.Values.Where(q => q.DateProcessing.HasValue == true);
    }

    public void UpdateOrder(Order order)
    {
        if (order != null && order.LineItems != null && order.LineItems.Count > 0)
            Orders[order.OrderId.Value] = order;

        return;
    }
}
