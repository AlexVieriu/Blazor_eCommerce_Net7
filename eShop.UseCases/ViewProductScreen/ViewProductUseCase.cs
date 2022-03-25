namespace eShop.UseCases.ViewProductScreen;
public class ViewProductUseCase : IViewProductUseCase
{
    private readonly IProductRepository _productRepo;

    public ViewProductUseCase(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public Product Execute(int id)
    {
        return _productRepo.GetProductbyId(id);
    }
}
