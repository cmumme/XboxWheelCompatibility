using ServiceWire.TcpIp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;
using XboxWheelCompatibility.CommunicationInterface;

namespace WheelCompatibilityConfigurator
{
    internal class Communicator
    {
        public TcpClient<IWheelCompatibilityService> TCPClient;

        public Communicator() {
            TCPClient = new TcpClient<IWheelCompatibilityService>(
                new TcpEndPoint(
                    new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"),
                        16581
                    )
                )
            );

            TCPClient.Proxy.GetMainWheelIndex();
        }
    }
}
