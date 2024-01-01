using Microsoft.Extensions.Configuration;
using System.IO;

namespace RapiddIdenity
{
    public static class DataAccessHelper
    {
        public static string GetConnectionString(string connectionStringKey = "")
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!string.IsNullOrEmpty(connectionStringKey))
            {
                return builder.Build().GetSection(connectionStringKey).Value;
            }
            else
            {
                return builder.Build().GetSection("ConnectionString").Value;
            }
        }
    }
}
