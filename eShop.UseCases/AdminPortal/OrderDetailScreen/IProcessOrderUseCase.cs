namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public interface IProcessOrderUseCase
{
    Task<bool> Execute(int orderId, string adminUserName);
}
