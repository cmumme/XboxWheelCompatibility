using System.Diagnostics;
using XboxWheelCompatibility.WheelCompatibilityService;


try
{
    EventLog.WriteEntry(".NET Runtime", "Starting the Wheel Compatibility Service", EventLogEntryType.Information, 1000);

    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<WheelCompatibilityWorker>();
    })
    .UseWindowsService()
    .Build();

    host.Run();

}
catch (Exception ex)
{
    EventLog.WriteEntry(".NET Runtime", ex.ToString(), EventLogEntryType.Error, 1000);
}