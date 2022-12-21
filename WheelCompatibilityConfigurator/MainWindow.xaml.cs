using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WheelCompatibilityConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Communicator ServiceCommunicator = new();

        public MainWindow()
        {
            InitializeComponent();
            UpdateServiceStatusIndicator();

            _ = LoopUpdates();
        }

        private async Task LoopUpdates()
        {
            while (true)
            {
                UpdateServiceStatusIndicator();
                await Task.Delay(1);
            }
        }

        private void UpdateServiceStatusIndicator()
        {
            ServiceStatusIndicator.Content = ServiceCommunicator.TCPClient.Proxy.GetMainWheelIndex() == -1 ? "No wheel connected" : "Wheel connected";
        }
    }
}
