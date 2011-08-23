using System;
using System.Configuration;
using System.Linq;
using Norm;

namespace ProMongoRepository
{
    public class ConfigurationParser
    {
        private const string DEFAULT_DATABASE = "admin";
        private const int DEFAULT_PORT = 27017;
        private const string PROTOCOL = "mongodb://";

        public static ConnectionStringBuilder LoadConfiguration(string connectionStringName)
        {
            ConnectionStringBuilder result;

            if (!string.IsNullOrEmpty(connectionStringName))
            {
                try
                {
                    result = ConnectionStringBuilder.Create(connectionStringName);
                    return result;
                }
                catch (NullReferenceException e)
                {
                    throw new MongoException("Connection String must start with 'mongodb://' or be the name of a connection string in the app.config.");
                }
            }

            if (ConfigurationManager.ConnectionStrings != null && ConfigurationManager.ConnectionStrings.Count > 0)
            {
                foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
                {
                    if (connection.ConnectionString.StartsWith(PROTOCOL, StringComparison.InvariantCultureIgnoreCase))
                    {
                        try
                        {
                            result = ConnectionStringBuilder.Create(connection.ConnectionString);
                            return result;
                        }
                        catch (NullReferenceException e)
                        {
                        }
                    }
                }
            }
            //if not exit use defaults.
            //collection = app name.
            //ConnectionString.Collection = GetType().GetGenericArguments()[0].Name;
            return null;
        }
    }
}