using System;

namespace JsonReferenceHandlerIssue
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public Summary Summary { get; set; }

        public City City { get; set; }
    }

    public class Summary
    {
        public string Description { get; set; }
    }

    public class City
    {
        public string Name { get; set; }
    }
}
