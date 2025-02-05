using CLARO_IV_API.Models;

namespace CLARO_IV_API.Interfaces.Categories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Insert(Category category);
        void Update(Category category);
        void Remove(Category category);
        void Delete(Category category);
    }
}