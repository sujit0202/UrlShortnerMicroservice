namespace UrlShortnerMicroservice.Services
{
    /// <summary>
    /// Interface layer for the URL Service..
    /// </summary>
    public interface IUrlShortnerService
    {
        /// <summary>
        /// Method which will perform the shorting of the long url provide.
        /// </summary>
        /// <param name="originalUrl">Instance of string</param>
        /// <returns>Instance of string indicating shortned url for lang url</returns>
        Task<string> ShortenUrlAsync(string originalUrl);

        /// <summary>
        /// Method will return long Url for given shortUrl
        /// </summary>
        /// <param name="shortCode">Instance of string.</param>
        /// <returns>Instance of string repreesenting the long url for given short Url.</returns>
        Task<string?> GetOriginalUrlAsync(string shortCode);
    }
}
