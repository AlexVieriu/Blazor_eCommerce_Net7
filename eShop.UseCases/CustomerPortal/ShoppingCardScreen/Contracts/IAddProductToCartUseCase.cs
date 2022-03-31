namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IAddProductToCartUseCase
{
    Task ExecuteAsync(Product product);
}
