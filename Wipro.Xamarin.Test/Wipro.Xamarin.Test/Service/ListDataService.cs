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
    public class ListDataService
    {
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
