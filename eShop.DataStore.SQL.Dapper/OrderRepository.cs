using Dapper;
using eShop.CoreBusiness.Models;
using eShop.DataStore.SQL.Dapper.Helpers;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
using System.Data;
using System.Data.SqlClient;

namespace eShop.DataStore.SQL.Dapper;
public class OrderRepository : IOrderRepository
{
    private readonly ISql _sql;

    public OrderRepository(ISql sql)
    {
        _sql = sql;
    }

    public async Task<int> CreateOrderAsync(Order order)
    {
        using IDbConnection connection = new SqlConnection();
        try
        {
            _sql.StartTransaction();

            DynamicParameters createOrder = new();

            createOrder.Add("DatePlaced", order.DatePlaced);
            createOrder.Add("DateProcessing", order.DateProcessing);
            createOrder.Add("DateProcessed", order.DateProcessed);
            createOrder.Add("CustomerName", order.CustomerName);
            createOrder.Add("CustomerAddress", order.CustomerAddress);
            createOrder.Add("CustomerCity", order.CustomerCity);
            createOrder.Add("CustomerStateProvince", order.CustomerStateProvince);
            createOrder.Add("AdminUser", order.AdminUser);
            createOrder.Add("UniqueId", order.UniqueId);

            createOrder.Add("OrderId", DbType.Int32, direction: ParameterDirection.Output);

            await _sql.SaveDataTransaction<dynamic>("sp_CreateOrder", createOrder);

            var idOrder = createOrder.Get<int>("OrderId");

            if (idOrder <= 0)
            {
                _sql.RollBackTransaction();
                return 0;
            }

            DynamicParameters createOrderLineItem = new();

            foreach (var linetItem in order.LineItems)
            {
                createOrderLineItem.Add("", linetItem.ProductId);
                createOrderLineItem.Add("", idOrder);
                createOrderLineItem.Add("", linetItem.Quantity);
                createOrderLineItem.Add("", linetItem.Price);

                await _sql.SaveDataTransaction<dynamic>("sp_CreateLineItem", createOrderLineItem);
            }

            _sql.CommitTransaction();
            return idOrder;
        }
        catch (Exception e)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<IEnumerable<OrderLineItem>> GetLineItemsByOrderIdAsync(int orderId)
    {
        try
        {

            return null;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Order GetOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderAsync(int orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetOrderByUniqueIdAsync(string uniquieId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetOutStandingsOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetProcessedOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}
