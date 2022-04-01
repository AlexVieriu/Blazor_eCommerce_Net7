namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IDeleteLineItemUseCase
{
    Task<Order> ExecuteAsync(int productId);
}
