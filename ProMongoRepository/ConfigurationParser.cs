using System;
using System.Configuration;
using MongoDB.Driver;

namespace ProMongoRepository
{
    public class ConfigurationParser
    {
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

            IConfigurationManager configuration = new ConfigurationManagerWrapper();
            var connectionStringSettingsCollection = configuration.GetConnectionStrings();
            if (connectionStringSettingsCollection != null && connectionStringSettingsCollection.Count > 0)
            {
                foreach (ConnectionStringSettings connection in connectionStringSettingsCollection)
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
            return ConnectionStringBuilder.Create(@"mongodb://localhost/MongoRepositoryDefault?strict=true");
        }
    }
}