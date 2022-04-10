namespace eShop.UseCases.CustomerPortal.ViewProductScreen;
public interface IAddProductToCartUseCase
{
    Task ExecuteAsync(Product product);
}
