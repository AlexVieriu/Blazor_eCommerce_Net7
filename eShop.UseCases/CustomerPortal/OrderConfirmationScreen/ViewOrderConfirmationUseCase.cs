namespace eShop.UseCases.CustomerPortal.OrderConfirmationScreen;
public class ViewOrderConfirmationUseCase : IViewOrderConfirmationUseCase
{
    private readonly IOrderRepository _orderRepo;

    public ViewOrderConfirmationUseCase(IOrderRepository orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public Order Execute(string uniqueId)
    {
        return _orderRepo.GetOrderByUniqueId(uniqueId);
    }
}
