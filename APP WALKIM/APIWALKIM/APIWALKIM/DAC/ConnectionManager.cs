﻿namespace APIWALKIM.DAC
{
    public class ConnectionManager
    {
        public static string getConnectionString()
        {
            var builder = new ConfigurationBuilder().
              SetBasePath(Directory.GetCurrentDirectory()).
              AddJsonFile("appsettings.json");

            var dbConnectionInfo = builder.Build().GetSection("ConnectionStrings").GetSection("WALKIM").Value;

            return dbConnectionInfo;
        }
    }
}
