using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace XboxWheelCompatibilityLibrary
{
    public class WheelManager
    {
        private static readonly object WheelListLock = new();
        public static readonly List<RacingWheel> ActiveWheels = new();
        public static event EventHandler<RacingWheel?>? MainWheelChanged;
        public static RacingWheel? MainWheel = null;

        public static void UpdateWheels()
        {
            Console.WriteLine("Updating wheel list");
            lock (WheelListLock)
            {

                foreach (var OldWheel in ActiveWheels.ToList())
                {
                    if (RacingWheel.RacingWheels.Contains(OldWheel)) continue;

                    ActiveWheels.Remove(OldWheel);
                }

                foreach (var NewWheel in RacingWheel.RacingWheels)
                {
                    if (ActiveWheels.Contains(NewWheel)) continue;

                    ActiveWheels.Add(NewWheel);
                }

                MainWheel = ActiveWheels.Count > 0 ? ActiveWheels.Last() : null;

                if(MainWheel != null) Console.WriteLine("Ready!");

                if (MainWheelChanged == null) return;
                MainWheelChanged.Invoke(null, MainWheel);
            }
        }

        private static void ListenForWheelChanges()
        {
            LifecycleManager.Tick += (object? Sender, EventArgs Event) =>
            {
                if(RacingWheel.RacingWheels.Count != ActiveWheels.Count)
                {
                    UpdateWheels();
                }
            };
        }

        public static void Initialize()
        {

            Console.WriteLine("Starting wheel manager");
            ListenForWheelChanges();
        }
    }
}
