namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Contracts;
public interface IViewShoppingCartUseCase
{
    Task<Order> ExecuteAsync();
}
