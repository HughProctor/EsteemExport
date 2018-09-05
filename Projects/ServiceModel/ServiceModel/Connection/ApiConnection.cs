using ServiceModel.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceModel.Connection
{
    public class ApiConnection
    {
        string _connectionString = null;
        string _connectionSettingName = "BAM_ConnectionString";

        public string GetConnectionString(string connectionSettingName = null)
        {
            _connectionSettingName = connectionSettingName ?? _connectionSettingName;

            var connectionStringSection = ConfigurationManager.ConnectionStrings[_connectionSettingName];

            return connectionStringSection.ConnectionString;
        }

        public ApiConnectionModel CreateConnection(string connectionString = null, string connectionSettingName = null)
        {
            _connectionSettingName = connectionString ?? _connectionSettingName;
            _connectionString = connectionString ?? GetConnectionString(_connectionSettingName);

            var connection = _connectionString.Split(';');
            var server = connection?.First(x => x.Contains("Server"))?.Split('=').Last();
            var returnValue = new ApiConnectionModel()
            {
                Server = connection?.First(x => x.Contains("Server"))?.Split('=').Last(),
                AuthorizationModel = new AuthorizationModel()
                {
                    UserName = connection?.First(x => x.Contains("UserName"))?.Split('=').Last(),
                    Password = connection?.First(x => x.Contains("Password"))?.Split('=').Last(),
                    LanguageCode = connection?.First(x => x.Contains("LanguageCode"))?.Split('=').Last(),
                }
            };

            return returnValue;
        }
    }
}
