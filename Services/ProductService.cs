using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using CLARO_IV_API.Repositories;

namespace CLARO_IV_API.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(ProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<ProductView>> GetAllAsync()
        {
            _logger.LogInformation($"Se ha accedido al metodo GetAll para obtener los productos");
            try
            {
                var products = await _repository.GetAllAsync();
                return GetProductsWithVariants(products);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error en GetAll (Products): {ex.Message}");
                return new List<ProductView>();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Se ha accedido al metodo GetById (Products) con el id #{id}");
            try
            {
                var product = await _repository.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"Producto #{id} no encontrado en el repositorio");
                    throw new NullReferenceException($"producto no encontrado por id #{id}");
                }
                _logger.LogInformation($"Producto #{id} - {product.Name} obtenido correctamente");
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en el metodo GetById para obtener el producto #{id}: {ex}");
                return new Product();
            }
        }

        public async Task<bool> InsertAsync(Product product)
        {
            _logger.LogInformation($"Se ha accedido al metodo Insert para agregar producto");
            try
            {
                return await _repository.InsertAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error en el metodo Insert: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {
                _logger.LogInformation($"Iniciando actualización de producto");
                return await _repository.UpdateAsync(product);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando el producto {product.Name}: {ex}");
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation($"Iniciando proceso de eliminacion del producto");
            try
            {
                return await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ha ocurrido un error eliminando el producto: {ex}");
                return false;
            }
        }

        internal List<ProductView> GetProductsWithVariants(IEnumerable<Product> products)
        {
            var result = products.Select(p => new ProductView
            {
                Id = p.Id,
                Name = p.Name,
                CategoryId = p.CategoryId,
                ProductVariants = p.ProductVariants,
                Colors = p.ProductVariants?.Count > 0 ? 
                p.ProductVariants.Select(v => v.Color).Distinct().ToList() : new List<string>(),
                PriceRange = p.ProductVariants?.Count > 0
                    ? $"{p.ProductVariants.Min(v => v.Price)}-{p.ProductVariants.Max(v => v.Price)}"
                    : "No disponible"
            }).ToList();
            return result;
        }

    }
}