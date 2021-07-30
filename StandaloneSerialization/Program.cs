using JsonReferenceHandlerIssue;
using JsonReferenceHandlerIssue.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandaloneSerialization
{
    class Program
    {
        static void Main()
        {
            // Returns array of 3000 forecast objects -- no repeating references
            WeatherForecast[] forecasts = new WeatherForecastController(null).Get().ToArray();

            // using a global ReferenceHandler mitigates the issue
            //var handler = new ReferenceHandler();

            while (true)
            {
                var handler = new ReferenceHandler();

                foreach (var forecast in forecasts)
                {
                    handler.GetReference(forecast, out _);
                    handler.GetReference(forecast.City, out _);
                    handler.GetReference(forecast.Summary, out _);
                }

                //handler.Reset();
            }
        }

        public class ReferenceHandler
        {
            private int _referenceCount = 0;
            private Dictionary<object, string> _objectToReferenceIdMap = new(ReferenceEqualityComparer.Instance);

            public string GetReference<T>(T value, out bool alreadyExists) where T : class
            {
                if (_objectToReferenceIdMap.TryGetValue(value, out string referenceId))
                {
                    alreadyExists = true;
                }
                else
                {
                    _referenceCount++;
                    referenceId = _referenceCount.ToString();
                    _objectToReferenceIdMap.Add(value, referenceId);
                    alreadyExists = false;
                }

                return referenceId;
            }

            public void Reset()
            {
                _objectToReferenceIdMap.Clear();
                _referenceCount = 0;
            }
        }
    }
}
