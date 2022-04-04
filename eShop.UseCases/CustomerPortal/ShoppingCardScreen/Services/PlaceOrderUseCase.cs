namespace eShop.UseCases.CustomerPortal.ShoppingCardScreen.Services;
public class PlaceOrderUseCase : IPlaceOrderUseCase
{
    private readonly IOrderService _orderService;
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCart _cart;
    private readonly IShoppingCartStateStore _stateStore;

    public PlaceOrderUseCase(IOrderService orderService,
                             IOrderRepository orderRepository,
                             IShoppingCart cart,
                             IShoppingCartStateStore stateStore)
    {
        _orderService = orderService;
        _orderRepository = orderRepository;
        _cart = cart;
        _stateStore = stateStore;
    }

    public async Task<string> ExecuteAsync(Order order)
    {
        if(_orderService.ValidateCreateOrder(order))
        {
            order.DatePlaced = DateTime.Now;
            order.UniqueId = Guid.NewGuid().ToString();
            _orderRepository.CreateOrder(order);

            await _cart.EmptyAsync();
            _stateStore.BroadCastStateChange();

            return order.UniqueId;
        }

        return string.Empty;
    }
}
