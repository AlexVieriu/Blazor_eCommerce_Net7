using eShop.CoreBusiness.Models;
using eShop.DataStore.SQL.Dapper.Helpers;
using eShop.UseCases.CustomerPortal.PluginInterfaces.DataStore;

namespace eShop.DataStore.SQL.Dapper;
public class ProductRepository : IProductRepository
{
    private readonly ISql _sql;

    public ProductRepository(ISql sql)
    {
        _sql = sql;
    }

    public async Task<Product> GetProductbyIdAsync(int id)
    {
        var entities = await _sql.LoadData<Product, dynamic>("sp_GetProductbyId", new { IdProduct = id });
        return entities.FirstOrDefault();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string filter)
    {
        if (string.IsNullOrEmpty(filter))
            filter = "%";

        var entities = await _sql.LoadData<Product, dynamic>("sp_GetProducts", new { Filter = filter });
        return entities;
    }
}
