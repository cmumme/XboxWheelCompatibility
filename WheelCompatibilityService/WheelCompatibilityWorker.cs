using ServiceWire.TcpIp;
using Windows.Gaming.Input;
using XboxWheelCompatibility.CommunicationInterface;
using XboxWheelCompatibility.WheelTransformer;

namespace XboxWheelCompatibility.WheelCompatibilityService
{
    public class WheelCompatibilityWorker : BackgroundService, IWheelCompatibilityService
    {
        private readonly ILogger<WheelCompatibilityWorker> Logger;
        private readonly TcpHost TCPHost;

        public int GetMainWheelIndex()
        {
            return RacingWheel.RacingWheels.ToList().IndexOf(WheelManager.MainWheel);
        }
        public void Start()
        {
            WheelInputTransformer.Start();
        }

        public void Stop()
        {
            WheelInputTransformer.Stop();
        }

        public WheelCompatibilityWorker(ILogger<WheelCompatibilityWorker> logger)
        {
            Logger = logger;
            TCPHost = new TcpHost(16581);
        }

        protected override Task ExecuteAsync(CancellationToken Cancellation)
        {
            TCPHost.AddService<IWheelCompatibilityService>(this);

            WheelInputTransformer.Start();

            TCPHost.Open();

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken Cancellation)
        {
            TCPHost.Close();

            WheelInputTransformer.Stop();

            return Task.CompletedTask;
        }
    }
}
