using NServiceBus;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sales
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Sales Endpoint";
            var endpointConfiguration = new EndpointConfiguration("Sales");
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endpointConfiguration);

            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
