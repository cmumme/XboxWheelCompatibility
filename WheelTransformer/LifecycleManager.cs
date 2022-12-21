using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XboxWheelCompatibility.WheelTransformer
{
    internal class LifecycleManager
    {
        public static EventHandler<EventArgs>? Tick;
        public static bool Started { get; private set; } = false;

        private static CancellationTokenSource CancellationSource = new();

        public static void Step()
        {
            Tick?.Invoke(null, EventArgs.Empty); ;
        }

        private static void Loop()
        {
            while (!CancellationSource.IsCancellationRequested)
            {
                Step();
            }
        }

        public static void Start()
        {
            if (CancellationSource.IsCancellationRequested) return;

            Task.Run(Loop, CancellationSource.Token);

            Started = true;
        }

        public static void Stop()
        {
            if (!CancellationSource.IsCancellationRequested) return;

            CancellationSource.Cancel();
        }
    }
}
