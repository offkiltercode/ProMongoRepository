using System.Configuration;

namespace ProMongoRepository
{
    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        public string ConnectionStrings(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return ConfigurationManager.ConnectionStrings;
        }

    }
}