using System.Data;

namespace Shared.Data.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IDbTransaction Transaction { get; }
    IDbConnection Connection { get; }
    void Begin();
    void Commit();
    void Rollback();
}