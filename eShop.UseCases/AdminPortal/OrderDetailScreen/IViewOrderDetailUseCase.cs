namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public interface IViewOrderDetailUseCase
{
    Task<Order> ExecuteAsync(int orderId);
}
