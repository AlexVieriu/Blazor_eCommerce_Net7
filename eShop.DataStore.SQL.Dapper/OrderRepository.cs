using Dapper;
using eShop.CoreBusiness.Models;
using eShop.DataStore.SQL.Dapper.Helpers;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;
using System.Data;

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
            _sql.StartTransaction();

            var lineItems = (await _sql.LoadDataTransation<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                                   new { OrderId = orderId })).ToList();
            var products = (await _sql.LoadDataTransation<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                                   new { OrderId = orderId })).ToList();
            _sql.CommitTransaction();

            foreach (var item in lineItems)
            {
                item.Product = products.Where(q => q.ProductId == item.ProductId).First();
            }

            return lineItems;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<Order> GetOrderAsync(int orderId)
    {
        try
        {
            var order = (await _sql.LoadData<Order, dynamic>("sp_GetOrderById", new { OrderId = orderId })).First();

            return order;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Order> GetOrderByUniqueIdAsync(string uniquieId)
    {
        try
        {
            var order = (await _sql.LoadData<Order, dynamic>("so_GetOrderByUniqueId",
                                                             new { OrderUniqueId = uniquieId })).First();

            return order;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetOutStandingsOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetOutStandingOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetProcessedOrdersAsync()
    {
        try
        {
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetProccessedOrders", new { });

            return orders;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task UpdateOrderAsync(Order order)
    {
        try
        {
            // update order
            DynamicParameters orderParams = new();

            orderParams.Add("AdminUser", order.AdminUser);
            orderParams.Add("CustomerAddress", order.CustomerAddress);
            orderParams.Add("CustomerCity", order.CustomerCity);
            orderParams.Add("CustomerCountry", order.CustomerCountry);
            orderParams.Add("CustomerName", order.CustomerName);
            orderParams.Add("CustomerStateProvince", order.CustomerStateProvince);
            orderParams.Add("DatePlaced", order.DatePlaced);
            orderParams.Add("DateProcessed", order.DateProcessed);
            orderParams.Add("DateProcessing", order.DateProcessing);
            orderParams.Add("UniqueId", order.UniqueId);
            orderParams.Add("OrderId", order.OrderId);

            await _sql.SaveDataTransaction<dynamic>("sp_UpdateOrder", orderParams);

            // update line Items
            DynamicParameters lineItemsParams = new();

            foreach (var lineItem in order.LineItems)
            {
                lineItemsParams.Add("", lineItem.Quantity);
                lineItemsParams.Add("", lineItem.Price);
                lineItemsParams.Add("", lineItem.ProductId);
                lineItemsParams.Add("", lineItem.OrderId);

                await _sql.SaveDataTransaction<dynamic>("sp_UpdateLineItem", lineItemsParams);
            }

            _sql.CommitTransaction();
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }
}
