using eShop.CoreBusiness.Models;
using eShop.UseCases.CustomerPortal.PluginInterfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace eShop.ShoppingCart.LocalStorage;

public class ShoppingCartBase : IShoppingCart
{
    private const string _constShoppingCart = "eShop.ShoppingCart";
    private readonly IJSRuntime _js;

    public ShoppingCartBase(IJSRuntime js)
    {
        _js = js;
    }

    public async Task AddProductToCartAsync(Product product)
    {
        // Get Order 
        var order = await GetOrderAsync();

        // Add Product
        if (order != null)
            order.AddProduct(product.ProductId, 1, product.Price, product);

        // SetOrder
        await SetOrderAsync(order);
    }

    public async Task<Order> DeleteProductFromCartAsync(int productId)
    {
        // GetOrder
        var order = await GetOrderAsync();

        // RemovePorduct
        if (order != null)
            order.RemoveProduct(productId);

        await SetOrderAsync(order);

        return order;
    }

    public Task EmptyAsync()
    {
        return SetOrderAsync(null);
    }

    public async Task<Order> GetOrderAsync()
    {
        Order order;
        var strOrder = await _js.InvokeAsync<string>("localStorage.getItem", _constShoppingCart);
        if (string.IsNullOrWhiteSpace(strOrder) || strOrder == "null")
        {
            order = new Order();
            await SetOrderAsync(order);
        }
        else
            order = JsonConvert.DeserializeObject<Order>(strOrder);

        return order;
    }

    public async Task SetOrderAsync(Order order)
    {
        await _js.InvokeVoidAsync("localStorage.setItem",
                                  _constShoppingCart,
                                  JsonConvert.SerializeObject(order));
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await SetOrderAsync(order);
    }

    public async Task<Order> UpdateQuantityAsync(int productId, int quantity)
    {
        var order = await GetOrderAsync();
        if (order != null)
        {
            if (quantity < 0)
                return order;

            else if (quantity == 0)
                order = await DeleteProductFromCartAsync(productId);

            else
            {
                var lineItem = order.LineItems.FirstOrDefault(x => x.ProductId == productId);
                if (lineItem != null)
                    lineItem.Quantity = quantity;
            }

            await SetOrderAsync(order);
        }

        return order;
    }
}
