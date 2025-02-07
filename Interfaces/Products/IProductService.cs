using CLARO_IV_API.Models;

namespace CLARO_IV_API.Interfaces.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductView>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<bool> InsertAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
    }
}