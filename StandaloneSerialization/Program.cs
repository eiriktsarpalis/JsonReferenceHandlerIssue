using JsonReferenceHandlerIssue;
using JsonReferenceHandlerIssue.Controllers;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StandaloneSerialization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press ESC or CTRL-C to stop");

            var stream = new MemoryStream();

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    MaxDepth = 32,
                    ReferenceHandler = ReferenceHandler.Preserve,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };

                WeatherForecast[] forecasts = new WeatherForecastController(null).Get().ToArray();

                stream.Position = 0;

                await JsonSerializer.SerializeAsync(stream, forecasts, options);
            }

            Console.WriteLine("Closing..");
        }
    }
}
