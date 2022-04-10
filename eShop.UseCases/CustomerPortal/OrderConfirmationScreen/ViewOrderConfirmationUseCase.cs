namespace eShop.UseCases.CustomerPortal.OrderConfirmationScreen;
public class ViewOrderConfirmationUseCase : IViewOrderConfirmationUseCase
{
    private readonly IOrderRepository _orderRepo;

    public ViewOrderConfirmationUseCase(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task<Order> ExecuteAsync(string uniqueId)
    {
        return await _orderRepo.GetOrderByUniqueIdAsync(uniqueId);
    }
}
