using System.Data;

namespace Shared.Data.Abstractions;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}