using JsonReferenceHandlerIssue.Controllers;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JsonReferenceHandlerIssue.ConsoleClient
{
    class Program
    {
        static async Task Main()
        {
            // referencing MVC defaults and settings from reproducing app
            // https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/JsonOptions.cs#L34-L40
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web) 
            {
                MaxDepth = 32,
                ReferenceHandler = ReferenceHandler.Preserve,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };

            var stream = new MemoryStream();
            WeatherForecast[] forecasts = new WeatherForecastController(null).Get().ToArray();
            
            while (true)
            {
                stream.Position = 0;
                await JsonSerializer.SerializeAsync(stream, forecasts, options);
            }
        }
    }
}
