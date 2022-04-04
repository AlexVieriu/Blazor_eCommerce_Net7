namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public interface IViewOrderDetailUseCase
{
    Order Execute(int orderId);
}
