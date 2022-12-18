using System;
using System.Collections.Generic;

namespace XboxWheelCompatibilityLibrary
{
    public class Program
    {
        public static void Main(string[] _)
        {

            LifecycleManager.Start();

            WheelManager.Initialize();
            InjectionManager.Initialize();

            Console.ReadLine();
        }

        public static void Stop(object Sender, EventArgs Event)
        {
            LifecycleManager.Stop();
            InjectionManager.Destroy();
        }
    }
}
