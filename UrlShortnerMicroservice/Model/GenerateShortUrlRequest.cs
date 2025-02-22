using System.ComponentModel.DataAnnotations;

namespace UrlShortnerMicroservice.Model
{
    /// <summary>
    /// Request data flow object generate short url method.
    /// </summary>
    public class GenerateShortUrlRequest
    {
        /// <summary>
        ///Instance of string represnting the longUrl.
        /// </summary>
   
        public string longUrl {  get; set; }

        /// <summary>
        /// Instance of string representing shortUrl.
        /// </summary>
        public string shortUrl {  get; set; }
    }
}
