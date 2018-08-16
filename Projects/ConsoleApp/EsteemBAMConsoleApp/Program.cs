using EntityModel.Mappers;
using SchedulerManager.Mechanism;

namespace EsteemBAMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Map.Init();
            JobManager jobManager = new JobManager();
            jobManager.ExecuteAllJobs();
        }
    }
}
