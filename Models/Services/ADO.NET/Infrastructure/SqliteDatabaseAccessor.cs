using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Corso10157.Models.Options;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Corso10157.Models.Services.ADO.NET.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {
        private readonly IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions;
        private readonly ILogger<SqliteDatabaseAccessor> logger;

        public SqliteDatabaseAccessor(IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions, ILogger<SqliteDatabaseAccessor> logger)
        {
            this.connectionStringsOptions = connectionStringsOptions;
            this.logger = logger;
        }
        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {
            /*PER REGISTRARE I LOG*/
            logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());
            
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameter = new List<SqliteParameter>();
            for (int i = 0; i <= queryArguments.Length - 1; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameter.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            using (var conn = new SqliteConnection(connectionStringsOptions.CurrentValue.Default))
            {
                await conn.OpenAsync();
                /*PER REGISTRARE I LOG*/
                logger.LogInformation("Connessione al DB stabilita");

                string query = formattableQuery.ToString();
                using (var cmd = new SqliteCommand(query, conn))
                {
                    cmd.Parameters.AddRange(sqliteParameter);
                    using (var read = await cmd.ExecuteReaderAsync())
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