namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IDeleteProductUseCase
{
    Task<Order> ExecuteAsync(int productId);
}
