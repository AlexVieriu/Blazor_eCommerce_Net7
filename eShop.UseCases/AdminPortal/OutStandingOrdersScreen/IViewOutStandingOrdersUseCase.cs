namespace eShop.UseCases.AdminPortal.OutStandingOrdersScreen;
public interface IViewOutStandingOrdersUseCase
{
    Task<List<Order>> ExecuteAsync();
}
