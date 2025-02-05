using CLARO_IV_API.Interfaces.Categories;
using CLARO_IV_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CLARO_IV_API.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductIvContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        public CategoryRepository(ProductIvContext context, ILogger<CategoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Category> GetAll()
        {
            _logger.LogInformation($"Trayendo información de las categorias registradas");
            try
            {
                return _context.Categories.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error trayendo las categorias registradas en la base de datos: {ex.Message}");
                return new List<Category>();
            }
        }

        public Category GetById(int id)
        {
            _logger.LogInformation($"Obteniendo categoria con ID #{id} de la base de datos");
           try
           {
            var category = _context.Categories.Find(id);
           if (category == null || category.IsDeleted)
           {
            _logger.LogWarning($"categoria #{id} no encontrada en la base de datos");
            throw new NullReferenceException($"categoria no encontrada por id #{id}");
           }
           return category;
           } catch (Exception ex)
           {
            _logger.LogError($"Error al acceder a la base de datos para obtener la categoria #{id}: {ex}");
            return new Category();
           }
        }

        public void Insert(Category category)
        {
            _logger.LogInformation($"Creando nueva categoria {category.Name} en la base de datos");
           try
           {
            _context.Categories.Add(category);
            _context.SaveChanges();
           } catch (Exception ex)
           {
             _logger.LogError($"Ha ocurrido un error insertando la categoria a la base de datos: {ex}");
           }
        }

        public void Update(Category category)
        {
            try
            {
                _logger.LogInformation($"Actualizando categoria en la base de datos");
                _logger.LogDebug($"Datos recibidos en categoria: {category.ToString()}");
                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();
            } catch(Exception ex)
            {
                    _logger.LogError($"Ha ocurrido un error actualizando la categoria {category.Name}: {ex}");
            }
        }
         public void Delete(Category category)
        {
            _logger.LogInformation($"Eliminando categoria {category.Name} de la base de datos");
            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Ha ocurrido un error eliminando la categoria {category.Name} de la base de datos: {ex}");
            }
        }

        public void Remove(Category category)
        {
            _logger.LogInformation($"Desactivando categoria {category.Name} de la base de datos (Soft Delete)");
            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error desactivando la categoria {category.Name} de la base de datos (Soft Delete): {ex}");
            }
        }
    }
}