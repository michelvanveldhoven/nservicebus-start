using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Shipping";
            var endppointConfiguration = new EndpointConfiguration("Shipping");
            var transport = endppointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endppointConfiguration);

            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
