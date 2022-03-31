namespace eShop.UseCases.CustomerPortal.ViewProductScreen;
public interface IViewProductUseCase
{
    Task<Product> ExecuteAsync(int productId);
}
