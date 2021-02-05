using System;
using System.Data;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public interface IDatabaseAccessor
    {
        DataSet Query(FormattableString query);
    }
}