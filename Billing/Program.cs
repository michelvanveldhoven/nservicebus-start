using NServiceBus;
using System;
using System.Threading.Tasks;

namespace Billing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Billing";
            var endppointConfiguration = new EndpointConfiguration("Billing");
            var transport = endppointConfiguration.UseTransport<LearningTransport>();
            var endpointInstance = await Endpoint.Start(endppointConfiguration);

            Console.WriteLine("Press Enter to quit");
            Console.ReadLine();

            await endpointInstance.Stop();
        }
    }
}
