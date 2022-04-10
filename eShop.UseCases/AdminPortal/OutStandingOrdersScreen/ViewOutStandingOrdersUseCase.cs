namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen;
public class ViewOutStandingOrdersUseCase : IViewOutStandingOrdersUseCase
{
    private readonly IOrderRepository _order;

    public ViewOutStandingOrdersUseCase(IOrderRepository order)
    {
        _order = order;
    }
    public async Task<List<Order>> ExecuteAsync()
    {
        return (await _order.GetOrdersAsync()).ToList();
    }
}
