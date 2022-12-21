using System.Diagnostics;

namespace XboxWheelCompatibility.WheelCompatibilityService
{

    public class WheelCompatibilityWorker : BackgroundService
    {
        private readonly ILogger<WheelCompatibilityWorker> Logger;

        public WheelCompatibilityWorker(ILogger<WheelCompatibilityWorker> logger)
        {
            Logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken Cancellation)
        {
            WheelTransformer.WheelInputTransformer.Start();

            Logger.LogInformation("Wheel input transformer has been started.");

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken Cancellation)
        {
            WheelTransformer.WheelInputTransformer.Stop();

            return Task.CompletedTask;
        }
    }
}
