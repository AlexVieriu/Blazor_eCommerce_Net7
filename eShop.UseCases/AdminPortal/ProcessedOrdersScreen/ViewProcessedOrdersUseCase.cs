namespace eShop.UseCases.AdminPortal.ProcessedOrdersScreen;
public class ViewProcessedOrdersUseCase : IViewProcessedOrdersUseCase
{
    private readonly IOrderRepository _order;

    public ViewProcessedOrdersUseCase(IOrderRepository order)
    {
        _order = order;
    }

    public List<Order> Execute()
    {
        return _order.GetProcessedOrders().ToList();
    }
}
