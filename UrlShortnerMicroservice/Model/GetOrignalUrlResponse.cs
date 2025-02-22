namespace UrlShortnerMicroservice.Model
{
    /// <summary>
    /// Reponse model class of 
    /// </summary>
    public class GetOrignalUrlResponse : GenerateShortUrlRequest
    {

        /// <summary>
        /// This will be filled only if any error of entry not present in DB.
        /// </summary>
        public string message { get; set; }
    }
}
