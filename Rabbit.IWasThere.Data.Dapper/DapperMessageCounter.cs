using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Rabbit.IWasThere.Data.Dapper
{
    public class DapperMessageCounter : IMessageCounter
    {
        private readonly SqlConnection _sqlConnection;

        public DapperMessageCounter(string connectionString)
            : this(new SqlConnection(connectionString))
        {
        }

        private DapperMessageCounter(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public IDictionary<Guid, int> CountMessages()
        {
            using (_sqlConnection)
            {
                return
                    _sqlConnection.Query("CountMessages", new { categoryId = Guid.Empty }, commandType: CommandType.StoredProcedure)
                        .ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount);
            }
        }

        public IDictionary<Guid, int> CountMessages(Guid categoryId)
        {
            using (_sqlConnection)
            {
                return
                    _sqlConnection.Query("CountMessages", new { categoryId = categoryId }, commandType: CommandType.StoredProcedure)
                        .ToDictionary(row => (Guid)row.CategoryId, row => (int)row.MessageCount);
            }
        }
    }
}
