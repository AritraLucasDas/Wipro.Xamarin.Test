using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wipro.Xamarin.Test.Model
{
    public partial class DataModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
    }

    public class Row
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageHref")]
        public string ImageHref { get; set; }
    }

    public partial class DataModel
    {
        public static DataModel FromJson(string json) => JsonConvert.DeserializeObject<DataModel>(json, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}




