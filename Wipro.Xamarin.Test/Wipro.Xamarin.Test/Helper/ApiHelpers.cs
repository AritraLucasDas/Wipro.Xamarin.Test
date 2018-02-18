using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Wipro.Xamarin.Test.Helper
{
    public static class ApiHelpers
    {
        public static HttpResponseMessage GenericGetMethod(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync(url).Result;    
        }
    }
}
