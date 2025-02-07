using CLARO_IV_API.Interfaces;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IGenericRepository<T> _repository;
    private readonly ILogger<IGenericService<T>> _logger;

    public GenericService(IGenericRepository<T> repository, ILogger<IGenericService<T>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        _logger.LogInformation($"Iniciando proceso para obtener todos los registros de la entidad {typeof(T).Name}");
        return await _repository.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        _logger.LogInformation($"Iniciando proceso para obtener la entidad {typeof(T).Name} #{id}");
        try 
        {
            return await _repository.GetByIdAsync(id);
        } catch(Exception)
        {
            throw;
        }
    }

    public async Task<bool> InsertAsync(T entity)
    {
        _logger.LogInformation($"Iniciando proceso para crear nueva entidad {typeof(T).Name}");
        return await _repository.InsertAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _logger.LogInformation($"Iniciando proceso para actualizar entidad {typeof(T).Name}");
        return await _repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation($"Iniciando proceso para eliminar entidad {typeof(T).Name}");
        return await _repository.DeleteAsync(id);
    }

    // public async Task<bool> DisableAsync(T entity)
    // {
    //     return await _repository.UpdateAsync(entity);
    // }
}
