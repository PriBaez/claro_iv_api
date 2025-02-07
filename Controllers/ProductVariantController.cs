using CLARO_IV_API.Interfaces;
using CLARO_IV_API.Interfaces.Products;
using CLARO_IV_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLARO_IV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VariantController : ControllerBase
    {
        private readonly IGenericService<ProductVariant> _service;
        private readonly ILogger<VariantController> _logger;

        public VariantController(IGenericService<ProductVariant> service, ILogger<VariantController> logger)
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
                    _logger.LogWarning("No se encontraron variantes");
                    return NotFound("No se encontraron variantes");
                }
                _logger.LogInformation("Retornando variantes encontradas");
                return Ok(product);
            } catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo variantes en el controlador: {ex}");
                return BadRequest($"Error obteniendo variantes: {ex.Message}");
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
                _logger.LogWarning($"variante no encontrada");
                return NotFound();
            }
            _logger.LogInformation("Retornando variante encontrada");
            return Ok(product);
           } catch(Exception ex)
           {
            _logger.LogError($"Error obteniendo variante en el controlador: {ex}");
            return BadRequest($"Ha ocurrido un error obteniendo el variante: {ex}");
           }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post(ProductVariant variant)
        {
            _logger.LogInformation($"Recibida solicitud POST a {Request.Path}");
            if (variant == null)
            {
                _logger.LogWarning($"Error en servicio: variante es una entidad nula");
                return BadRequest("Error en servicio: variante es una entidad nula");
            }
            try
            {
                bool insertProcess = await _service.InsertAsync(variant);
                if(!insertProcess)
                {
                    _logger.LogError($"Error creando la variante: el servicio devolvió {insertProcess}");
                    return BadRequest($"Ocurrió un error durante el proceso de creación de la variante");
                }
                _logger.LogInformation("Variante creada correctamente");
                return StatusCode(201, "Variante creada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creando variante de ${variant.Product?.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrio un error creando la variante: {ex}");
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductVariant variant)
        {
            _logger.LogInformation($"Recibida solicitud PUT a {Request.Path}");
            if (id != variant.Id || variant == null)
            {
                _logger.LogWarning($"Error: hay discrepancia en los datos o la variante es una entidad nula");
                return BadRequest("Error: hay una discrepancia en los datos o la variante es una entidad nula");
            }
            try
            {
                bool updateProcess = await _service.UpdateAsync(variant);
                if(!updateProcess)
                {
                    _logger.LogError($"Error actualizando la variante: el servicio devolvió {updateProcess}");
                    return BadRequest($"Ocurrió un error durante el proceso de actualización de la variante");
                }
                _logger.LogInformation($"Variante actualizado correctamente");
                return NoContent();
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error actualizando la variante ${variant.Product?.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrió un error actualizando la variante: {ex}");
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
                    _logger.LogWarning($"La variante #{id} no fue encontrada");
                    return NotFound($"La variante #{id} no fue encontrada");
                }

                if(product.Id != id)
                {
                    _logger.LogWarning($"La variante tiene una disparidad de datos");
                    return NotFound($"La variante tiene una disparidad de datos");
                }

                bool deleteProcess = await _service.DeleteAsync(id);
                if(!deleteProcess)
                {
                    _logger.LogError($"Error eliminando la variante: el servicio devolvió {deleteProcess}");
                    return BadRequest($"Ocurrió un error durante el proceso de eliminación de la variante");
                }
                _logger.LogInformation($"Variante eliminado correctamente");
                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error en la eliminación de la variante: {ex.Message}");
                return BadRequest($"Ha ocurrido un error en la eliminación de la variante: {ex.Message}");
            }
        }
    }
}