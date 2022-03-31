namespace eShop.UseCases.CustomerPortal.SearchProductScreen;
public class SearchProductUseCase : ISearchProductUseCase
{
    private readonly IProductRepository _productRepo;

    public SearchProductUseCase(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<IEnumerable<Product>> ExecuteAsync(string filter)
    {
        return await _productRepo.GetProductsAsync(filter);
    }
}
