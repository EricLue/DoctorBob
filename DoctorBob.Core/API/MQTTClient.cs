using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;
using DoctorBob.Core.OrderManagement.Domain;
using Newtonsoft.Json;
using Serilog;

namespace DoctorBob.Core.API
{
    public class MQTTClient
    {

        public static void Main(List<string> commandsList)
        {
            // Creates a new client
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                                    .WithClientId("Dev.To")
                                                    .WithTcpServer("10.100.201.21", 1883);
            // Create client options objects
            ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                    .WithClientOptions(builder.Build())
                                    .Build();
            // Creates the client object
            IManagedMqttClient _mqttClient = new MqttFactory().CreateManagedMqttClient();
            // Set up handlers
            _mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(OnConnected);
            _mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(OnDisconnected);
            _mqttClient.ConnectingFailedHandler = new ConnectingFailedHandlerDelegate(OnConnectingFailed);
            // Starts a connection with the Broker
            _mqttClient.StartAsync(options).GetAwaiter().GetResult();

            foreach (string command in commandsList)
            {
                _mqttClient.PublishAsync(command);
            }
        }

        public static void OnConnected(MqttClientConnectedEventArgs obj)
        {
            Log.Logger.Information("Successfully connected.");
        }

        public static void OnConnectingFailed(ManagedProcessFailedEventArgs obj)
        {
            Log.Logger.Warning("Couldn't connect to broker.");
        }

        public static void OnDisconnected(MqttClientDisconnectedEventArgs obj)
        {
            Log.Logger.Information("Successfully disconnected.");
        }
    }
}
