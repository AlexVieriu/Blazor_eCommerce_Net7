namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public class ViewOrderDetailUseCase : IViewOrderDetailUseCase
{
    private readonly IOrderRepository _order;

    public ViewOrderDetailUseCase(IOrderRepository order)
    {
        _order = order;
    }

    public Order Execute(int orderId)
    {
        return _order.GetOrder(orderId);
    }
}
