using System;
using System.Data;
using System.Threading.Tasks;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public interface IDatabaseAccessor
    {
        // DataSet Query(FormattableString query);
        Task<DataSet> QueryAsync(FormattableString query);
    }
}
