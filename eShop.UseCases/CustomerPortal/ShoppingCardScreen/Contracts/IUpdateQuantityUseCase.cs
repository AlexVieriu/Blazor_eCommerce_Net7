namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IUpdateQuantityUseCase
{
    Task<Order> ExecuteAsync(int productId, int qty);
}
