using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CLARO_IV_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductIvContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ProductIvContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Product> GetAll()
        {
            _logger.LogInformation($"Trayendo información de los productos registrados");
            try
            {
                return _context.Products.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error trayendo los productos registrados en la base de datos: {ex.Message}");
                return new List<Product>();
            }
        }

        public Product GetById(int id)
        {
            _logger.LogInformation($"Obteniendo producto con ID #{id}");
           try
           {
            var product = _context.Products.Find(id);
           if (product == null || product.IsDeleted)
           {
            _logger.LogWarning($"Producto #{id} no encontrado en la base de datos");
            throw new NullReferenceException($"producto no encontrado por id #{id}");
           }
           return product;
           } catch (Exception ex)
           {
            _logger.LogError($"Error al acceder a la base de datos para obtener el producto #{id}: {ex}");
            return new Product();
           }
        }

        public void Insert(Product product)
        {
            _logger.LogInformation($"Creando nuevo producto {product.Name} en la base de datos");
           try
           {
            _context.Products.Add(product);
            _context.SaveChanges();
           } catch (Exception ex)
           {
             _logger.LogError($"Ha ocurrido un error insertando el producto a la base de datos: {ex}");
           }
        }

        public void Update(Product product)
        {
            try
            {
                _logger.LogInformation($"Actualizando producto en la base de datos");
                _logger.LogInformation($"Datos recibidos en producto: {product.ToString()}");
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
            } catch(Exception ex)
            {
                    _logger.LogError($"Ha ocurrido un error actualizando el producto {product.Name}: {ex}");
            }
        }
         public void Delete(Product product)
        {
            _logger.LogInformation($"Eliminando producto {product.Name} de la base de datos");
            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Ha ocurrido un error eliminando el producto {product.Name} de la base de datos: {ex}");
            }
        }

        public void Remove(Product product)
        {
            _logger.LogInformation($"Desactivando producto {product.Name} de la base de datos (Soft Delete)");
            try
            {
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error desactivando el producto {product.Name} de la base de datos (Soft Delete): {ex}");
            }
        }
    }
}