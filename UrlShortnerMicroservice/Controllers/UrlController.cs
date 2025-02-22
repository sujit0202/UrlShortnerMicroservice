using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortnerMicroservice.Model;
using UrlShortnerMicroservice.Services;

namespace UrlShortnerMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        /// <summary>
        /// Instance of IUrlShortnerService.
        /// </summary>
        private IUrlShortnerService _urlShortnerService;

        public UrlController(IUrlShortnerService urlShortnerService) 
        { 
             _urlShortnerService = urlShortnerService;
        }

        /// <summary>
        /// This will provide a short url for given long url.
        /// </summary>
        /// <param name="request">Instance of GenerateShortRequest</param>
        /// <returns>On completion will return the long url with valid statuscode.</returns>
        [HttpPost("generateShortenUrl")]
        public async Task<IActionResult> generateShortUrl([FromBody] GenerateShortUrlRequest request)
        {
            if (string.IsNullOrEmpty(request.longUrl)) 
            {
                return BadRequest("the longUrl needs to be specified");
            }
            try
            {
                var shortUrl = await _urlShortnerService.ShortenUrlAsync(request.longUrl);
                GenerateShortUrlResponse generateShortUrlResponse = new GenerateShortUrlResponse();
                generateShortUrlResponse.longUrl = request.longUrl;
                generateShortUrlResponse.shortUrl = shortUrl;

                return Created(new Uri(""), generateShortUrlResponse);
            }
            catch (Exception ex) 
            {
               return StatusCode(500, "Internal server error. Unable to process request");
            }
        }

        /// <summary>
        /// Retrives the orignal URL associated with the short URL.
        /// </summary>
        /// <param name="request">Instance of GetOrignalUrlRequest containing the short URL to which find the orignal URL.</param>
        /// <returns>Response with short URL and long URL with message if not found it will be error message.</returns>        
        [HttpPost("getOrignalUrl")]
        public async Task<IActionResult> getOrignalUrl([FromBody] GetOrinalUrlRequest request) 
        {
            if (string.IsNullOrEmpty(request.shortUrl))
            {
                return BadRequest("Short URL property cant be empty or null");
            }

            try
            {
            var LongUrlResponse = await _urlShortnerService.GetOriginalUrlAsync(request.shortUrl);

            GetOrignalUrlResponse getOrignalUrlResponse = new GetOrignalUrlResponse();
            getOrignalUrlResponse.shortUrl = request.shortUrl;
            getOrignalUrlResponse.longUrl = LongUrlResponse;

            if (LongUrlResponse != null) 
            {
                getOrignalUrlResponse.message = request.shortUrl + "is not present into the database";
                    return NotFound(getOrignalUrlResponse);
            }
                else
                {
                    return Ok(getOrignalUrlResponse);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error occeured while processing request." + ex.Message);
            }
        }
    }
}
