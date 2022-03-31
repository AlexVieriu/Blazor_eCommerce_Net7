namespace eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;

public interface IProductRepository
{
    Task<Product> GetProductbyIdAsync(int id);
    Task<IEnumerable<Product>> GetProductsAsync(string filter);
}
