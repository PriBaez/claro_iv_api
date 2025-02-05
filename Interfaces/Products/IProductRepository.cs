using CLARO_IV_API.Models;

namespace CLARO_IV_API.Interfaces.Products
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Remove(Product product);
    }
}