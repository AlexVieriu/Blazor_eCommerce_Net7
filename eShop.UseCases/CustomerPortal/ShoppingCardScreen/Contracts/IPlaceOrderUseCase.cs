namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IPlaceOrderUseCase
{
    Task<string> ExecuteAsync(Order order);
}
