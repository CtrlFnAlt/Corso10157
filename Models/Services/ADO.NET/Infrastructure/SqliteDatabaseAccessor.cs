using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        public DataSet Query(FormattableString formattableQuery)
        {
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameter = new List<SqliteParameter>();
            for (int i = 0; i <= queryArguments.Length - 1; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameter.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            using (var conn = new SqliteConnection("Data Source=Data/Data.db"))
            {
                conn.Open();
                string query = formattableQuery.ToString();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameter);
                    using (var read = cmd.ExecuteReader())
                    {
                        var dataSet = new DataSet();
                        dataSet.EnforceConstraints = false;
                        do
                        {
                            var dataTable = new DataTable();
                            dataSet.Tables.Add(dataTable);
                            dataTable.Load(read);
                        } while (!read.IsClosed);
                        return dataSet;
                    }
                }
            }
        }
    }
}