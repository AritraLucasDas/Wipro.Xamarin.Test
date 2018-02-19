using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Wipro.Xamarin.Test.Helper
{
    /// <summary>
    /// This class holds the helper methods related to API calls
    /// </summary>
    public static class ApiHelpers
    {
        /// <summary>
        /// This is a helper method to make GET calls
        /// </summary>
        /// <param name="url"> Complete URL with arguments to make the gate call</param>
        /// <returns> HttpResponseMessage recieved from the server </returns>
        public static HttpResponseMessage GenericGetMethod(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync(url).Result;    
        }
    }
}
