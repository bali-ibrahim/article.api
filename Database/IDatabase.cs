using System.Data;

namespace Database
{
    public interface IDatabase
    {
        public IDbConnection Connection { get; }
    }
}