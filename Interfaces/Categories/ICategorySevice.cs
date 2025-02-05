using CLARO_IV_API.Models;

namespace CLARO_IV_API.Interfaces.Categories
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        bool Insert(Category category);
        bool Update(Category category);
        bool Remove(Category category);
        bool Delete(Category category);
    }
}