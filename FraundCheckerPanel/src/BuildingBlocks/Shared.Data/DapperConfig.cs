using Dapper;

namespace Shared.Data;

public static class DapperConfig
{
    public static void Configure()
    {
        // This allows Dapper to map user_id (SQL) to UserId (C#).
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}