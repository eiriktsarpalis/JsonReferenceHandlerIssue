using System;

namespace JsonReferenceHandlerIssue.WebClient.Interface
{
    /// <summary>The Picturepark service settings interface.</summary>
    public interface IWebClientServiceSettings
    {
        #region Properties

        /// <summary>Gets the <see cref="IAuthClient"/>.</summary>
        IAuthClient AuthClient { get; }

        /// <summary>Gets the server URL of the Picturepark authentication server.</summary>
        string BaseUrl { get; }

        /// <summary>Gets the customer alias.</summary>
        string CustomerAlias { get; }

        /// <summary>Gets the display language.</summary>
        string DisplayLanguage { get; }

        /// <summary>Gets the HTTP timeout.</summary>
        TimeSpan HttpTimeout { get; }

        #endregion
    }
}
