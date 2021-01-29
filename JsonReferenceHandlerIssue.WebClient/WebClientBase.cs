using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using JsonReferenceHandlerIssue.WebClient.Interface;

namespace JsonReferenceHandlerIssue.WebClient
{
    public abstract class WebClientBase
    {
        #region Fields

        private readonly IWebClientServiceSettings _settings;

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="ClientBase" /> class.</summary>
        /// <param name="settings">The client settings.</param>
        protected WebClientBase(IWebClientServiceSettings settings)
        {
            _settings = settings;
        }

        #endregion

        #region Properties

        /// <summary>Gets the base URL of the Picturepark API.</summary>
        public string BaseUrl => _settings.BaseUrl;

        /// <summary>Gets the used customer alias.</summary>
        public string CustomerAlias => _settings.CustomerAlias;

        #endregion

        #region Methods

        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var message = new HttpRequestMessage();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(_settings.CustomerAlias))
            {
                message.Headers.TryAddWithoutValidation("WarehouseClientService-CustomerAlias", _settings.CustomerAlias);
            }

            if (_settings.AuthClient != null)
            {
                foreach (var header in await _settings.AuthClient.GetAuthenticationHeadersAsync().ConfigureAwait(false))
                    message.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (!string.IsNullOrEmpty(_settings.DisplayLanguage))
            {
                message.Headers.TryAddWithoutValidation("WarehouseClientService-Language", _settings.DisplayLanguage);
            }

            return message;
        }

        #endregion
    }
}
