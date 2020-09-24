using Microsoft.Extensions.Configuration;
using System.Data;
using Database;
using MySql.Data.MySqlClient;

namespace Maria
{
    public class Maria : IDatabase
    {
        private readonly IConfiguration _configuration;

        public IDbConnection Connection => GetConnection();

        public Maria(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            var connectionString = _configuration.GetConnectionString("maria");
            return new MySqlConnection(connectionString);
        }
    }
}