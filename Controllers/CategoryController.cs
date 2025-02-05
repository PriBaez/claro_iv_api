using CLARO_IV_API.Interfaces.Categories;
using CLARO_IV_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CLARO_IV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService service, ILogger<CategoryController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"Recibida solicitud GET a {Request.Path}");
            try
            {
                var category = _service.GetAll();
                if (category == null || !category.Any())
                {
                    _logger.LogWarning("No se encontraron categorias");
                    return NotFound("No se encontraron categorias");
                }
                _logger.LogInformation("Retornando categorias encontradas");
                return Ok(category);
            } catch (Exception ex)
            {
                _logger.LogError($"Error obteniendo categorias en el controlador: {ex}");
                return BadRequest($"Error obteniendo categorias: {ex.Message}");
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation($"Recibida solicitud GET a {Request.Path}");
           try
           {
            if (id == 0)
            {
                _logger.LogWarning($"Error: ID nulo o igual a cero");
                return BadRequest("Ocurrió un error durante la petición: ID nulo o igual a cero");
            }
            
            var category = _service.GetById(id);

            if (category == null)
            {
                _logger.LogWarning($"Categoria no encontrada");
                return NotFound();
            }
            _logger.LogInformation("Retornando categoria encontrada");
            return Ok(category);
           } catch(Exception ex)
           {
            _logger.LogError($"Error obteniendo categoria en el controlador: {ex}");
            return BadRequest($"Ha ocurrido un error obteniendo la categoria: {ex}");
           }
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post(Category category)
        {
            _logger.LogInformation($"Recibida solicitud POST a {Request.Path}");
            if (category == null)
            {
                _logger.LogWarning($"Error en servicio: categoria es una entidad nula");
                return BadRequest("Error en servicio: categoria es una entidad nula");
            }
            try
            {
                _service.Insert(category);
                _logger.LogInformation("Categoria creada correctamente");
                return StatusCode(201, "Categoria creada exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creando categoria ${category.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrio un error creando la categoria: {ex}");
            }
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Category category)
        {
            _logger.LogInformation($"Recibida solicitud PUT a {Request.Path}");
            if (id != category.Id || category == null)
            {
                _logger.LogWarning($"Error: hay discrepancia en los datos o la categoria es una entidad nula");
                return BadRequest("Error: hay una discrepancia en los datos o la categoria es una entidad nula");
            }
            try
            {
                _service.Update(category);
                _logger.LogInformation($"Categoria actualizada correctamente");
                return NoContent();
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Error actualizando la categoria ${category.Name} en el controlador: {ex}");
                return StatusCode(400, $"Ocurrió un error actualizando la categoria: {ex}");
            }
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Recibida solicitud DELETE a {Request.Path}");
                var category = _service.GetById(id);
                if (category == null)
                {
                    _logger.LogWarning($"La categoria #{id} es nula");
                    return NotFound($"La categoria #{id} es nula");
                }

                bool deleteProcess = _service.Delete(category);
                if(!deleteProcess)
                {
                    _logger.LogError($"Error eliminando la categoria: el servicio devolvió {deleteProcess}");
                    return BadRequest($"Ocurrio un error durante el proceso de eliminación de la categoria");
                }
                _logger.LogInformation($"Categoria eliminada correctamente");
                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error en la eliminación de la categoria: {ex.Message}");
                return BadRequest($"Ha ocurrido un error en la eliminación de la categoria: {ex.Message}");
            }
        }

        [HttpDelete("Remove/{id}")]
        //Soft Delete
        public IActionResult Remove(int id)
        {
            try
            {
                _logger.LogInformation($"Recibida solicitud DELETE a {Request.Path}");
                var category = _service.GetById(id);
                if (category == null)
                {
                    _logger.LogWarning($"La categoria #{id} no fue encontrado");
                    return NotFound($"La categoria #{id} no fue encontrado");
                }
                bool deleteProcess = _service.Remove(category);
                if(!deleteProcess)
                {
                    _logger.LogError($"Ha ocurrido un error desactivación de la categoria");
                    return BadRequest($"Ha ocurrido un error en la eliminación de la categoria");
                }
                _logger.LogInformation($"Categoria desactivada correctamente");
                return NoContent();
            } catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido en la desactivación de la categoria: {ex.Message}");
                return BadRequest($"Ha ocurrido un error en la eliminación de la categoria: {ex.Message}");
            }
        }
    }
}