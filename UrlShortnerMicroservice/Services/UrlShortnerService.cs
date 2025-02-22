
using System;
using Microsoft.EntityFrameworkCore;
using UrlShortnerMicroservice.Data; 
using UrlShortnerMicroservice.Model;

namespace UrlShortnerMicroservice.Services
{
    public class UrlShortnerService : IUrlShortnerService
    {
        private UrlShortnerContext _context = new UrlShortnerContext();

        private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
       
        private Random _random = new Random();

        /// <summary>
        /// Retrives the orignal URL associated with the given short URL.
        /// </summary>
        /// <param name="shortUrl">Instance of string indicating the short URL.</param>
        /// <returns>The orignal URL if found; otherwise null.</returns>
        public async Task<string?> GetOriginalUrlAsync(string shortUrl)
        {
            var response = await _context.UrlMappings.FirstOrDefaultAsync(s => s.ShortUrl == shortUrl);
            if (response != null)
            {
                return response.LongUrl;
            }
            return null;
        }

        /// <summary>
        /// Shortness an orignal URL by generating a unique code and storing into database.
        /// </summary>
        /// <param name="originalUrl">Instance of string indicating the orignal URL.</param>
        /// <returns>The newley generated short URL.</returns>
        public async Task<string> ShortenUrlAsync(string originalUrl)
        {
            var shortcode = GenerateShortCode();
            var shortUrl = "newgen.ly" + shortcode;

            var mapping = new UrlMapping();
            mapping.ShortUrl = shortUrl;
            mapping.LongUrl = originalUrl;

            var response = await _context.UrlMappings.AddAsync(mapping);

            await _context.SaveChangesAsync();
            return response.Entity.ShortUrl;
        }

        private string GenerateShortCode(int length = 6)
        {
            return new string(Enumerable.Repeat(Alphabet, length).Select(s => s [_random.Next(s.Length)]).ToArray());
        }
    }
}
