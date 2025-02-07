using CLARO_IV_API.Interfaces;
using CLARO_IV_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CLARO_IV_API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T: class
    {
        private readonly ProductIvContext _context;
        private readonly ILogger<GenericRepository<T>> _logger;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ProductIvContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            _logger.LogInformation($"Trayendo información de {typeof(T).Name} registradas");
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error trayendo {typeof(T).Name} de la base de datos: {ex.Message}");
                return new List<T>();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Obteniendo {typeof(T).Name} con ID #{id} de la base de datos");
           try
           {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
           {
            _logger.LogWarning($"{typeof(T).Name} #{id} no se encuentra en la base de datos");
            throw new NullReferenceException($"{typeof(T).Name} no se encuentra por id #{id}");
           }
            return entity;
           } catch (Exception ex)
           {
            _logger.LogError($"Error al acceder a la base de datos para obtener la entidad {typeof(T).Name}  #{id}: {ex}");
            throw;
           }
        }

        public async Task<bool> InsertAsync(T entity)
        {
            _logger.LogInformation($"Creando {typeof(T).Name} en la base de datos");
           try
           {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
           } catch (Exception ex)
           {
             _logger.LogError($"Ha ocurrido un error creando {typeof(T).Name} en la base de datos: {ex}");
             return false;
           }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _logger.LogInformation($"Actualizando {typeof(T).Name} en la base de datos");
                _logger.LogDebug($"Datos recibidos en {typeof(T).Name}: {entity.ToString()}");
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return true;
            } catch(Exception ex)
            {
                _logger.LogError($"Ha ocurrido un error actualizando {typeof(T).Name}: {ex}");
                return false;
            }
        }
         public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation($"Eliminando {typeof(T).Name} de la base de datos");
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if(entity == null) return false;

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {  
                _logger.LogError($"Ha ocurrido un error eliminando la entidad {typeof(T).Name} de la base de datos: {ex}");
                return false;
            }
        }

        // public async Task<bool> DisableAsync(T entity)
        // {
        //     _logger.LogInformation($"Desactivando categoria {typeof(T).Name}  en la base de datos (Soft Delete)");
        //     try
        //     {
        //         _dbSet.Update(entity);
        //         await _context.SaveChangesAsync();
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError($"Ha ocurrido un error desactivando la entidad {typeof(T).Name}  en la base de datos (Soft Delete): {ex}");
        //         return true;
        //     }
        // }
    }
}