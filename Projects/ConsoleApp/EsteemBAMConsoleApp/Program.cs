using SchedulerManager.Mechanism;

namespace EsteemBAMConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JobManager jobManager = new JobManager();
            jobManager.ExecuteAllJobs();
        }
    }
}
