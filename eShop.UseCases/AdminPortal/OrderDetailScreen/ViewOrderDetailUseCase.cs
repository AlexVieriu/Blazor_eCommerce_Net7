namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public class ViewOrderDetailUseCase : IViewOrderDetailUseCase
{
    private readonly IOrderRepository _order;

    public ViewOrderDetailUseCase(IOrderRepository order)
    {
        _order = order;
    }

    public async Task<Order> ExecuteAsync(int orderId)
    {
        return await _order.GetOrderAsync(orderId);
    }
}
