using System.Collections.Generic;
using System.Threading.Tasks;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Core.Services;

public class BaseService<TEntity> : IService<TEntity>
    where TEntity : class
{
    private readonly IRepository<TEntity> _repository;

    public BaseService(IRepository<TEntity> repository)
    {
        _repository = repository;
    }
    public async Task<OperationResult<IReadOnlyList<TEntity>>> GetAllAsync()
    {
        return new OperationResult<IReadOnlyList<TEntity>>(await _repository.GetAllAsync());
    }
}