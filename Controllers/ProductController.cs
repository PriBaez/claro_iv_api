using CLARO_IV_API.Interfaces;
using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLARO_IV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation($"Recibida solicitud GET a {Request.Path}");
            try
            {
                var product = await _service.GetAllAsync();
                if (product == null || !product.Any())
                {
                    _logger.LogWarning("No se encontraron productos");
                    return NotFound("No se encontraron productos");
                }
                _logger.LogInformation("Retornando productos encontrados");
                return Ok(product);
            } catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo productos en el controlador: {ex}");
                return BadRequest($"Error obteniendo productos: {ex.Message}");
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Recibida solicitud GET a {Request.Path}");
           try
           {
            if (id == 0)
            {
                _logger.LogWarning($"Error: ID nulo o igual a cero");
                return BadRequest("Ocurrió un error durante la petición: ID nulo o igual a cero");
            }
            
            var product = await _service.GetByIdAsync(id);

            if (product == null)
            {
                _logger.LogWarning($"producto no encontrado");
                return NotFound();
            }
            _logger.LogInformation("Retornando producto encontrado");
            return Ok(product);
           } catch(Exception ex)
           {
            _logger.LogError($"Error obteniendo producto en el controlador: {ex}");
            return BadRequest($"Ha ocurrido un error obteniendo el producto: {ex}");
           }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            _logger.LogInformation($"Recibida solicitud POST a {Request.Path}");
            if (product == null)
            {
                _logger.LogWarning($"Error en servicio: producto es una entidad nula");
                return BadRequest("Error en servicio: producto es una entidad nula");
            }
            try
            {
                bool insertProcess = await _service.InsertAsync(product);
                if(!insertProcess)
                {
                    _logger.LogError($"Error creando el producto: el servicio devolvió {insertProcess}");
                    return BadRequest($"Ocurrió un error durante el proceso de creación del producto");
                }
                _logger.LogInformation("Producto creado correctamente");
                return StatusCode(201, "Producto creado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creando producto ${product.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrio un error creando el producto: {ex}");
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Product product)
        {
            _logger.LogInformation($"Recibida solicitud PUT a {Request.Path}");
            if (id != product.Id || product == null)
            {
                _logger.LogWarning($"Error: hay discrepancia en los datos o el producto es una entidad nula");
                return BadRequest("Error: hay una discrepancia en los datos o el producto es una entidad nula");
            }
            try
            {
                bool updateProcess = await _service.UpdateAsync(product);
                if(!updateProcess)
                {
                    _logger.LogError($"Error actualizando el producto: el servicio devolvió {updateProcess}");
                    return BadRequest($"Ocurrió un error durante el proceso de actualización del producto");
                }
                _logger.LogInformation($"producto actualizado correctamente");
                return NoContent();
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error actualizando el producto ${product.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrió un error actualizando el producto: {ex}");
            }
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Recibida solicitud DELETE a {Request.Path}");
                var product = _service.GetByIdAsync(id);
                if (product == null)
                {
                    _logger.LogWarning($"el producto #{id} no fue encontrado");
                    return NotFound($"el producto #{id} no fue encontrado");
                }

                if(product.Id != id)
                {
                    _logger.LogWarning($"El producto tiene una disparidad de datos");
                    return NotFound($"El producto tiene una disparidad de datos");
                }

                bool deleteProcess = await _service.DeleteAsync(id);
                if(!deleteProcess)
                {
                    _logger.LogError($"Error eliminando el producto: el servicio devolvió {deleteProcess}");
                    return BadRequest($"Ocurrio un error durante el proceso de eliminación del producto");
                }
                _logger.LogInformation($"producto eliminado correctamente");
                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error en la eliminación del producto: {ex.Message}");
                return BadRequest($"Ha ocurrido un error en la eliminación del producto: {ex.Message}");
            }
        }
    }
}