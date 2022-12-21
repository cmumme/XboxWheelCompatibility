using System;
using System.Collections.Generic;

namespace XboxWheelCompatibility.WheelTransformer
{
    public class WheelInputTransformer
    {
        public static void Start()
        {
            LifecycleManager.Start();

            WheelManager.Initialize();
            InjectionManager.Initialize();
        }

        public static void Stop()
        {
            LifecycleManager.Stop();
            InjectionManager.Destroy();
        }
    }
}
