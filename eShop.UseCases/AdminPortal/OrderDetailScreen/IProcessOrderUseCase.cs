namespace eShop.UseCases.AdminPortal.OrderDetailScreen;
public interface IProcessOrderUseCase
{
    Task<bool> ExecuteAsync(int orderId, string adminUserName);
}
