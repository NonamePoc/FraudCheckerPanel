using Shared.Data.Abstractions;
using System.Data;

namespace Shared.Data;

public abstract class BaseRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IUnitOfWork? _unitOfWork;

    protected BaseRepository(IDbConnectionFactory connectionFactory, IUnitOfWork? unitOfWork = null)
    {
        _connectionFactory = connectionFactory;
        _unitOfWork = unitOfWork;
    }

    protected async Task<T> QueryAsync<T>(Func<IDbConnection, IDbTransaction?, Task<T>> queryFunc)
    {
        if (_unitOfWork?.Transaction != null)
        {
            return await queryFunc(_unitOfWork.Connection, _unitOfWork.Transaction);
        }

        using var connection = _connectionFactory.CreateConnection();
        return await queryFunc(connection, null);
    }
    
    protected async Task ExecuteAsync(Func<IDbConnection, IDbTransaction?, Task> queryFunc)
    {
        if (_unitOfWork?.Transaction != null)
        {
            await queryFunc(_unitOfWork.Connection, _unitOfWork.Transaction);
            return;
        }

        using var connection = _connectionFactory.CreateConnection();
        await queryFunc(connection, null);
    }
}