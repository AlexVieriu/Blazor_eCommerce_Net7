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

    public async Task<bool> ExecuteAsync(int orderId, string adminUserName)
    {
        var order = await _orderRepo.GetOrderAsync(orderId);
        order.AdminUser = adminUserName;
        order.DateProcessed = DateTime.Now;

        if (!_orderService.ValidateUpdateOrder(order))
        {
            await _orderRepo.UpdateOrderProccedAsync(adminUserName, order.DateProcessed, orderId);
            return true;
        }
        return false;
    }
}
