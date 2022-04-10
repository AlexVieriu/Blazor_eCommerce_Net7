namespace eShop.UseCases.AdminPortal.ProcessedOrdersScreen;
public class ViewProcessedOrdersUseCase : IViewProcessedOrdersUseCase
{
    private readonly IOrderRepository _order;

    public ViewProcessedOrdersUseCase(IOrderRepository order)
    {
        _order = order;
    }

    public async Task<List<Order>> ExecuteAsync()
    {
        return (await _order.GetProcessedOrdersAsync()).ToList();
    }
}
