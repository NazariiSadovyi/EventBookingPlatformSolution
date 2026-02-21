using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EBP.Infrastructure.Internal
{
    internal static class DbUpdateExceptionExtensions
    {
        internal static bool IsUniqueViolation(this DbUpdateException ex, string uniqueFieldName)
        {
            if (ex.InnerException is not SqlException sqlEx)
                return false;

            // 2601 = unique index duplicate, 2627 = unique constraint violation
            var isUnique = sqlEx.Number == 2601 || sqlEx.Number == 2627;

            var mentionsIndex = sqlEx.Message.Contains(uniqueFieldName, StringComparison.OrdinalIgnoreCase);

            return isUnique && mentionsIndex;
        }
    }
}
