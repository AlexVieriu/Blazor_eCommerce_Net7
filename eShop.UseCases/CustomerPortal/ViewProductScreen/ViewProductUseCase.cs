namespace eShop.UseCases.CustomerPortal.ViewProductScreen;
public class ViewProductUseCase : IViewProductUseCase
{
    private readonly IProductRepository _productRepo;

    public ViewProductUseCase(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }
    public async Task<Product> ExecuteAsync(int productId)
    {
        return await _productRepo.GetProductbyIdAsync(productId);
    }
}
