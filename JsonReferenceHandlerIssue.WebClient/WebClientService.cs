using System;
using System.Net.Http;
using JsonReferenceHandlerIssue.WebClient.Interface;

namespace JsonReferenceHandlerIssue.WebClient
{
    public class WebClientService : IWebClientService, IDisposable
    {
        #region Fields

        private HttpClient _httpClient;

        private bool disposedValue = false;

        #endregion

        #region Constructors

        public WebClientService(IWebClientServiceSettings settings)
        {
            // TODO: Investigate Polly for error handling: https://github.com/App-vNext/Polly
            _httpClient = new HttpClient(new WebClientRetryHandler()) { Timeout = settings.HttpTimeout };

            Initialize(settings, _httpClient);
        }

        public WebClientService(IWebClientServiceSettings settings, HttpClient httpClient)
        {
            Initialize(settings, httpClient);
        }

        #endregion

        #region Properties

        public IWeatherForecastClient WeatherForecastClient { get; private set; }

        #endregion

        #region Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_httpClient != null)
                    {
                        _httpClient.Dispose();
                        _httpClient = null;
                    }
                }

                disposedValue = true;
            }
        }

        private void Initialize(IWebClientServiceSettings settings, HttpClient httpClient)
        {
            WeatherForecastClient = new WeatherForecastClient(settings, httpClient);
        }

        #endregion
    }
}
