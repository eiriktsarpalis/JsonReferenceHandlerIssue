using System;
using System.Collections.Generic;
using System.Text;

namespace JsonReferenceHandlerIssue.WebClient.Interface
{
    public interface IWebClientService
    {
        #region Properties

        IWeatherForecastClient WeatherForecastClient { get; }

        #endregion
    }
}
