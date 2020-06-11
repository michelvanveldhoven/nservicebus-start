using Messages;
using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Threading.Tasks;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            Console.Title = "NSeviceBus Client UI";
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");
            routing.RouteToEndpoint(typeof(CancelOrder), "Sales");

            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            
            Console.WriteLine("Press Enter to exit!");
            Console.ReadLine();

            await RunLoop(endpointInstance);

            await endpointInstance.Stop();
        }

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            var lastOrder = string.Empty;

            while (true)
            {
                log.Info("Press 'P' to place an order, Press 'C' to cancel a placed order, or 'Q' to quit the program");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        var command = new PlaceOrder
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };
                        log.Info($"Sending PlaceOrder command, OrderId {command.OrderId}");
                        //await endpointInstance.SendLocal(command);
                        lastOrder = command.OrderId;
                        await endpointInstance.Send(command);
                        break;
                    case ConsoleKey.C:
                        var cancelCommand = new CancelOrder
                        {
                            OrderId = lastOrder
                        };
                        await endpointInstance.Send(cancelCommand);
                        log.Info($"Sent a correlated message to {cancelCommand.OrderId}");
                        break;
                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown command. Please try again");
                        break;
                }

            }
        }
    }
}
