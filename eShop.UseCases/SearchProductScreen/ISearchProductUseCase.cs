namespace eShop.UseCases.SearchProductScreen;
public interface ISearchProductUseCase
{
    Task<IEnumerable<Product>> ExecuteAsync(string filter = null);
}
