using CLARO_IV_API.Models;

namespace CLARO_IV_API.Interfaces.Products
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        bool Insert(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Remove(Product product);
    }
}