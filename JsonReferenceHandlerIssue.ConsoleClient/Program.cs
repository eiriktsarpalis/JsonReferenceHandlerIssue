using JsonReferenceHandlerIssue.WebClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JsonReferenceHandlerIssue.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var clientService = new WebClientService(new WebClientServiceSettings("https://localhost:5001", string.Empty));

            Console.WriteLine("Press ESC or CTRL-C to stop");

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                try
                {
                    var forecast = await clientService.WeatherForecastClient.GetAsync();
                }
                catch (System.Net.Http.HttpRequestException)
                {
                }

                await Task.Delay(1000);
            }

            Console.WriteLine("Closing..");
        }
    }
}
