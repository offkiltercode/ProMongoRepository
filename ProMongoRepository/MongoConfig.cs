namespace ProMongoRepository
{
    public class MongoConfig {

        public string Server { get; set; }
        public int? Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Query { get; set; }
        public string ConnectionString { get; set; }
        public string Collection { get; set; }

        //public string ConnectionString {

        //    get {

        //        string authentication = string.Empty;
        //        if (!string.IsNullOrEmpty(Username)) {
        //            authentication = string.Concat(Username, ':', Password, '@');
        //        }
        //        if (!string.IsNullOrEmpty(Query) && !Query.StartsWith("?")) {
        //            Query = string.Concat('?', Query);
        //        }

        //        if (string.IsNullOrEmpty(Server)) { Server = "localhost"; }
        //        if (Port == null) { Port = 27017; }
        //        if (string.IsNullOrEmpty(Database)) { Database = "mongo"; }

        //        var connectionString = string.Format("mongodb://{0}{1}:{2}/{3}{4}", authentication, Server, Port, Database, Query);
        //        //_logger.Debug(connectionString);
        //        return connectionString;

        //    } 
        //}
    }
}

