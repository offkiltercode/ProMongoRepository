using System.Configuration;

namespace ProMongoRepository
{
    public interface IConfigurationManager
    {
        string ConnectionStrings(string name);
        ConnectionStringSettingsCollection GetConnectionStrings();

    }
}