using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Rabbit.IWasThere.Data.Dapper
{
    public class DapperMessageCounter : IMessageCounter
    {
        private readonly string _connectionString;

        public DapperMessageCounter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDictionary<Guid, int> CountMessages()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var commandBuilder = new StringBuilder();
                commandBuilder.Append("exec CountMessages @categoryId;");
                commandBuilder.Append("exec CountAllMessages;");

                var command = new CommandDefinition(commandBuilder.ToString(), new { categoryId = Guid.Empty });

                var resultReader = sqlConnection.QueryMultiple(command);

                var byCategoriesResult = resultReader.Read().ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount);
                var totalResult = resultReader.Read().ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount);

                return byCategoriesResult.Union(totalResult).ToDictionary(x => x.Key, x => x.Value);
            }
        }

        public int CountMessages(Guid categoryId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return
                    sqlConnection.Query("CountMessages", new { categoryId = categoryId },
                        commandType: CommandType.StoredProcedure)
                        .ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount).First().Value;
            }
        }

        public int CountAllMessages()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return
                    sqlConnection.Query("CountAllMessages", commandType: CommandType.StoredProcedure)
                        .ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount).First().Value;
            }
        }
    }
}
