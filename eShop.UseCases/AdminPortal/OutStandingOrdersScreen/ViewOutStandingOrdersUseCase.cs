namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen;
public class ViewOutStandingOrdersUseCase : IViewOutStandingOrdersUseCase
{
    private readonly IOrderRepository _order;

    public ViewOutStandingOrdersUseCase(IOrderRepository order)
    {
        _order = order;
    }
    public List<Order> Execute()
    {
        return _order.GetOrders().ToList();
    }
}
