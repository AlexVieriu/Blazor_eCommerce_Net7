namespace eShop.UseCases.AdminPortal.ProcessedOrdersScreen;
public interface IViewProcessedOrdersUseCase
{
    Task<List<Order>> ExecuteAsync();
}
    
