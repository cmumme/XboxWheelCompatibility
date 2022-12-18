using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using Windows.UI.Input.Preview.Injection;

namespace XboxWheelCompatibilityLibrary
{
    public class InjectionManager
    {
        private static readonly InputInjector Injector = InputInjector.TryCreate();

        private static void InjectCurrentReading()
        {
            
            if (WheelManager.MainWheel == null) return;

            RacingWheelReading WheelReading = WheelManager.MainWheel.GetCurrentReading();

            Injector.InjectGamepadInput(new InjectedInputGamepadInfo(
                    new GamepadReading(
                        (ulong)DateTime.UtcNow.Ticks,
                        (GamepadButtons)(
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.DPadDown) ? (int)GamepadButtons.DPadDown : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.DPadUp) ? (int)GamepadButtons.DPadUp : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.DPadLeft) ? (int)GamepadButtons.DPadLeft : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.DPadRight) ? (int)GamepadButtons.DPadRight : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.NextGear) ? (int)GamepadButtons.RightShoulder : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.PreviousGear) ? (int)GamepadButtons.LeftShoulder : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button1) ? (int)GamepadButtons.Menu : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button2) ? (int)GamepadButtons.View : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button3) ? (int)GamepadButtons.A : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button4) ? (int)GamepadButtons.B : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button5) ? (int)GamepadButtons.X : 0) +
                            (WheelReading.Buttons.HasFlag(RacingWheelButtons.Button6) ? (int)GamepadButtons.Y : 0)
                        ),
                        WheelReading.Brake, WheelReading.Throttle,
                        WheelReading.Wheel, 0,
                        0, 0
                    )
                )
            );
        }

        public static void Initialize()
        {
            Console.WriteLine("Starting injection manager");

            Injector.InitializeGamepadInjection();

            LifecycleManager.Tick += (object? Sender, EventArgs Event) =>
            {
                if (RacingWheel.RacingWheels.Count > WheelManager.ActiveWheels.Count)
                {
                    WheelManager.UpdateWheels();
                }

                if (WheelManager.MainWheel == null)
                {
                    Console.WriteLine($"no main wheel. active wheels: {WheelManager.ActiveWheels.Count} inactive wheels: {RacingWheel.RacingWheels.Count} rawgamecontrollers detected: {RawGameController.RawGameControllers.Count}");

                    return;
                };

                InjectCurrentReading();
            };
        }

        public static void Destroy()
        {
            Injector.UninitializeGamepadInjection();
        }
    }
}
