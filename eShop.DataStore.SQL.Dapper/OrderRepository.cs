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
            createOrder.Add("CustomerCountry", order.CustomerCountry);
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

            DynamicParameters lineItemsParams = new();

            foreach (var lineItem in order.LineItems)
            {
                lineItemsParams.Add("ProductId", lineItem.ProductId);
                lineItemsParams.Add("OrderId", idOrder);
                lineItemsParams.Add("Quantity", lineItem.Quantity);
                lineItemsParams.Add("Price", lineItem.Price);

                lineItemsParams.Add("LineItemId", DbType.Int32, direction: ParameterDirection.Output);

                await _sql.SaveDataTransaction<dynamic>("sp_CreateLineItem", lineItemsParams);

                var lineItemID = lineItemsParams.Get<int>("LineItemId");

                if (lineItemID <= 0)
                {
                    _sql.RollBackTransaction();
                    return 0;
                }
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
            _sql.StartTransaction();

            var order = (await _sql.LoadDataTransation<Order, dynamic>("sp_GetOrderById",
                                                             new { OrderId = orderId })).FirstOrDefault();

            order.LineItems = (await _sql.LoadDataTransation<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                          new { OrderId = orderId })).ToList();

            var products = (await _sql.LoadDataTransation<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                       new { OrderId = orderId })).ToList();

            if (order.OrderId < 0)
            {
                _sql.RollBackTransaction();
                return null;
            }

            _sql.CommitTransaction();

            foreach (var item in order.LineItems)
            {
                item.Product = products.Where(q => q.ProductId == item.ProductId).First();
            }

            return order;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
            throw;
        }
    }

    public async Task<Order> GetOrderByUniqueIdAsync(string uniqueId)
    {
        try
        {
            _sql.StartTransaction();
            var order = (await _sql.LoadDataTransation<Order, dynamic>("sp_GetOrderByUniqueId",
                                                             new { UniqueId = uniqueId })).FirstOrDefault();

            order.LineItems = (await _sql.LoadDataTransation<OrderLineItem, dynamic>("sp_GetLineItemsByOrderId",
                                                                          new { OrderId = order.OrderId })).ToList();

            var products = (await _sql.LoadDataTransation<Product, dynamic>("sp_GetLineItemsByOrderId",
                                                                       new { OrderId = order.OrderId })).ToList();

            if (order.OrderId < 0)
            {
                _sql.RollBackTransaction();
                return null;
            }

            _sql.CommitTransaction();

            foreach (var item in order.LineItems)
            {
                item.Product = products.Where(q => q.ProductId == item.ProductId).First();
            }

            return order;
        }
        catch (Exception ex)
        {
            _sql.RollBackTransaction();
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
            var orders = await _sql.LoadData<Order, dynamic>("sp_GetProcessedOrders", new { });

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
            _sql.StartTransaction();

            // update order
            DynamicParameters orderParams = new();

            orderParams.Add("AdminUser", order.AdminUser);
            orderParams.Add("CustomerAddress", order.CustomerAddress);
            orderParams.Add("CustomerCity", order.CustomerCity);
            orderParams.Add("CustomerCountry", order.CustomerCountry);
            orderParams.Add("CustomerName", order.CustomerName);
            orderParams.Add("CustomerStateProvince", order.CustomerStateProvince);
            orderParams.Add("UniqueId", order.UniqueId);
            orderParams.Add("OrderId", order.OrderId);

            await _sql.SaveDataTransaction<dynamic>("sp_UpdateOrder", orderParams);

            // update line Items
            DynamicParameters lineItemsParams = new();

            foreach (var lineItem in order.LineItems)
            {
                lineItemsParams.Add("ProductId", lineItem.ProductId);
                lineItemsParams.Add("OrderId", lineItem.OrderId);
                lineItemsParams.Add("Quantity", lineItem.Quantity);
                lineItemsParams.Add("Price", lineItem.Price);

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

    public async Task UpdateOrderProccedAsync(string adminUser, DateTime? dateProcced, int orderId)
    {
        try
        {
            DynamicParameters orderParams = new();

            orderParams.Add("AdminUser", adminUser);
            orderParams.Add("DateProcced", dateProcced);
            orderParams.Add("OrderId", orderId);

            await _sql.SaveData<dynamic>("sp_UpdateProccedOrder", orderParams);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
