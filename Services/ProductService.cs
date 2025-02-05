using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;

namespace CLARO_IV_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Product> GetAll()
        {
            _logger.LogInformation($"Se ha accedido al metodo GetAll para obtener los productos");
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error en GetAll (Products): {ex.Message}");
                return new List<Product>();
            }
        }

        public Product GetById(int id)
        {
            _logger.LogInformation($"Se ha accedido al metodo GetById (Products) con el id #{id}");
           try
           {
            var product = _repository.GetById(id);
           if (product == null || product.IsDeleted)
           {
            _logger.LogWarning($"Producto #{id} no encontrado en el repositorio");
            throw new NullReferenceException($"producto no encontrado por id #{id}");
           }
           _logger.LogInformation($"Producto #{id} - {product.Name} obtenido correctamente");
           return product;
           } catch (Exception ex)
           {
            _logger.LogError($"Error en el metodo GetById para obtener el producto #{id}: {ex}");
            return new Product();
           }
        }

        public bool Insert(Product product)
        {
            _logger.LogInformation($"Se ha accedido al metodo Insert para agregar producto");
           try
           {
            _repository.Insert(product);
            _logger.LogInformation("Producto insertado correctamente");
            return true;
           } catch (Exception ex)
           {
             _logger.LogError($"Ha ocurrido un error en el metodo Insert: {ex}");
             return false;
           }
        }

        public bool Update(Product product)
        {
            try
            {
                _logger.LogInformation($"Iniciando actualización de producto");
                _repository.Update(product);
                _logger.LogInformation("Producto actualizado correctamente");
                return true;
            } catch(Exception ex)
            {
                    _logger.LogError($"Ha ocurrido un error actualizando el producto {product.Name}: {ex}");
                    return false;
            }
        }
         public bool Delete(Product product)
        {
            _logger.LogInformation($"Iniciando proceso de eliminacion del producto {product.Name}");
            try
            {
                _repository.Delete(product);
                _logger.LogError($"Producto eliminado correctamente");
                return true;
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Ha ocurrido un error eliminando el producto {product.Name}: {ex}");
                return false;
            }
        }

        public bool Remove(Product product)
        {
            _logger.LogInformation($"Desactivando producto {product.Name} (Soft Delete)");
            try
            {
                _repository.Remove(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error desactivando el producto {product.Name} (Soft Delete): {ex}");
                return false;
            }
        }
    }
}