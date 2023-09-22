
using GBReaderJacqueminS.Repositories;
using System.Data;
using System.Data.Common;

namespace GBReaderJacqueminS.Infrastructures
{

    public class DBRepositoryFactory
    {
        private readonly DbProviderFactory _factory;
        private readonly string _connectionString;
        public DBRepositoryFactory(string providerName, string connectionString)
        {
            try
            {
                DbProviderFactories.RegisterFactory(providerName, MySql.Data.MySqlClient.MySqlClientFactory.Instance);
                _factory = DbProviderFactories.GetFactory(providerName);
                _connectionString = connectionString;
            }
            catch (ArgumentException ex)
            {
                throw new UnableToConnectException("création de la factory a échoué", ex);
            }

        }

        public static DBRepositoryFactory CreateSession()
        {
            return new DBRepositoryFactory("MySql.Data.MySqlClient", @"server=192.168.128.13;uid=in20b1012;pwd=1730;database=in20b1012");
        }

        public DBRepository NewStorage()
        {
            try
            {
                IDbConnection con = _factory.CreateConnection();
                con.ConnectionString = _connectionString;
                con.Open();
                return new DBRepository(con);
            }
            catch (Exception ex)
            {
                throw new UnableToConnectException("création de la connection a échoué", ex);
            }

        }

    }
}
