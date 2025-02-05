using CLARO_IV_API.Interfaces.Categories;
using CLARO_IV_API.Models;

namespace CLARO_IV_API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(ICategoryRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Category> GetAll()
        {
            _logger.LogInformation($"Se ha accedido al metodo GetAll para obtener las categorias");
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error en GetAll para obtener las categorias: {ex.Message}");
                return new List<Category>();
            }
        }

        public Category GetById(int id)
        {
            _logger.LogInformation($"Se ha accedido al metodo GetById para obtener categoria con el id #{id}");
           try
           {
            var category = _repository.GetById(id);
           if (category == null || category.IsDeleted)
           {
            _logger.LogWarning($"Categoria #{id} no encontrada en el repositorio");
            throw new NullReferenceException($"Categoria no encontrada por id #{id}");
           }
           _logger.LogInformation($"Categoria #{id} - {category.Name} obtenida correctamente");
           return category;
           } catch (Exception ex)
           {
            _logger.LogError($"Error en el metodo GetById para obtener la categoria #{id}: {ex}");
            return new Category();
           }
        }

        public bool Insert(Category category)
        {
            _logger.LogInformation($"Se ha accedido al metodo Insert para agregar categoria");
           try
           {
            _repository.Insert(category);
            _logger.LogInformation("Categoria insertada correctamente");
            return true;
           } catch (Exception ex)
           {
             _logger.LogError($"Ha ocurrido un error en el metodo Insert para agregar categoria {category.Name}: {ex}");
             return false;
           }
        }

        public bool Update(Category category)
        {
            try
            {
                _logger.LogInformation($"Se ha accedido al metodo Update para actualizar categoria");
                _repository.Update(category);
                _logger.LogInformation("Categoria actualizada correctamente");
                return true;
            } catch(Exception ex)
            {
                    _logger.LogError($"Ha ocurrido un error en el metodo Insert para actualizar categoria {category.Name}: {ex}");
                    return false;
            }
        }
         public bool Delete(Category Category)
        {
            _logger.LogInformation($"Se ha accedido al metodo Update para eliminar categoria");
            try
            {
                _repository.Delete(Category);
                _logger.LogError($"Categoria eliminada correctamente");
                return true;
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Ha ocurrido un error en el metodo Delete para eliminar categoria {Category.Name}: {ex}");
                return false;
            }
        }

        public bool Remove(Category Category)
        {
            _logger.LogInformation($"Se ha accedido al metodo Remove para desactivar categoria (Soft Delete)");
            try
            {
                _repository.Remove(Category);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error en el metodo Remove para desactivar categoria {Category.Name} (Soft Delete): {ex}");
                return false;
            }
        }
    }
}