using Microsoft.EntityFrameworkCore;
using ProductsWebApi.Dal;

namespace ProductsWebApi.Services
{
  public interface IStoreService
  {
    Task<(Store store, int productCount)> GetWithMaxProductsAsync();
  }

  public class StoreService : IStoreService
  {
    private readonly ProductDbContext _dbContext;

    public StoreService(ProductDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<(Store store, int productCount)> GetWithMaxProductsAsync()
    {

      var storeWithMaxProducts = await _dbContext.Products
        .GroupBy(p => p.StoreId)
        .Select(g => new { StoreId = g.Key, ProductCount = g.Count() })
        .OrderByDescending(g => g.ProductCount)
        .FirstOrDefaultAsync()
        ;

      if (storeWithMaxProducts == null)
      {
        return (null, 0);
      }

      Store store = await _dbContext.Stores.FirstOrDefaultAsync(s => s.Id == storeWithMaxProducts.StoreId);

      return (store, storeWithMaxProducts.ProductCount);
    }

  }
}
