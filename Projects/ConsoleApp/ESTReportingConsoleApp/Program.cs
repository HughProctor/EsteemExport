using ESTReporting.EntityModel.Context;
using System.Configuration;

namespace ESTReportingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["BAMEsteemExportDb"].ConnectionString;
            using (var db = new BAMEsteemExportContext(connectionString))
            {

            }
        }
    }
}
