using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wipro.Xamarin.Test.Helper;
using Wipro.Xamarin.Test.Model;

namespace Wipro.Xamarin.Test.Service
{
    /// <summary>
    /// This class is to handle all the services related to the ListPage
    /// </summary>
    public class ListDataService
    {
        /// <summary>
        /// This method is to get actual data to feed the list on UI, from external source
        /// </summary>
        /// <returns> A DataModel object recieved from the JSON</returns>
        public DataModel GetListData()
        {
            DataModel result = null;
            HttpResponseMessage response =
                ApiHelpers.GenericGetMethod("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
            if (response.IsSuccessStatusCode)
            {
                var responseString = response.Content.ReadAsStringAsync().Result;
                result = JsonConvert.DeserializeObject<DataModel>(responseString);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }

            return result;
        }
    }
}
