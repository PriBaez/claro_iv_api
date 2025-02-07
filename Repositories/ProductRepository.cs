using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CLARO_IV_API.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        private readonly ProductIvContext _context;
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ProductIvContext context, ILogger<ProductRepository> logger) 
        : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }

        public new virtual async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation($"Trayendo información de los productos registrados");
            try
            {
                var products = await _context.Products.Include(x => x.ProductVariants).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error trayendo los productos registrados en la base de datos: {ex.Message}");
                return new List<Product>();
            }
        }
    }
}