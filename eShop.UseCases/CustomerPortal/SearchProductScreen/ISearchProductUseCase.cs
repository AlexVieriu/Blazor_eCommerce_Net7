namespace eShop.UseCases.CustomerPortal.SearchProductScreen;
public interface ISearchProductUseCase
{
    Task<IEnumerable<Product>> ExecuteAsync(string? filter = null);
}
