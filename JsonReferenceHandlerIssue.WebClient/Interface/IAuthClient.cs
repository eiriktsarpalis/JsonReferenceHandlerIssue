using System.Collections.Generic;
using System.Threading.Tasks;

namespace JsonReferenceHandlerIssue.WebClient.Interface
{
    /// <summary>Retrieves access tokens for authentication.</summary>
    public interface IAuthClient
    {
        #region Properties

        /// <summary>Gets the server URL of the Picturepark authentication server.</summary>
        string BaseUrl { get; }

        /// <summary>Gets the customer alias.</summary>
        string CustomerAlias { get; }

        #endregion

        #region Methods

        /// <summary>Gets the authentication headers.</summary>
        /// <returns>The headers.</returns>
        Task<IDictionary<string, string>> GetAuthenticationHeadersAsync();

        #endregion
    }
}
