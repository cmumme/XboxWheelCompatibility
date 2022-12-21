using System.Diagnostics;


namespace XboxWheelCompatibility.WheelCompatibilityService
{
    class Program
    {
        public static void Main(string[] Arguments)
        {

            try
            {
                Host.CreateDefaultBuilder(Arguments)
                    .UseWindowsService()
                    .ConfigureServices(Services =>
                        {
                            Services.AddHostedService<WheelCompatibilityWorker>();
                        }
                    )
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(".NET Runtime", ex.ToString(), EventLogEntryType.Error, 1000);
            }
        }
    }
}