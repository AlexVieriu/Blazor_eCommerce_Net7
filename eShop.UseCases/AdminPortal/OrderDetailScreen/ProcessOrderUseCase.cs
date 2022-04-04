namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public class ProcessOrderUseCase : IProcessOrderUseCase
{
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderService _orderService;

    public ProcessOrderUseCase(IOrderRepository orderRepo, IOrderService orderService)
    {
        _orderRepo = orderRepo;
        _orderService = orderService;
    }

    public bool Execute(int orderId, string adminUserName)
    {
        var order = _orderRepo.GetOrder(orderId);
        order.AdminUser = adminUserName;
        order.DateProcessed = DateTime.Now;

        if (!_orderService.ValidateUpdateOrder(order))
        {
            _orderRepo.UpdateOrder(order);
            return true;
        }
        return false;
    }
}
