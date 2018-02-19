using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wipro.Xamarin.Test.Model
{
    /// <summary>
    /// This class is a model class to handle data needed for this project, this
    /// contains a list of row objects and the title of the data
    /// </summary>
    public partial class DataModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("rows")]
        public List<Row> Rows { get; set; }
    }

    /// <summary>
    /// This is a model class for the rows data in the DataModel class
    /// </summary>
    public class Row
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("imageHref")]
        public string ImageHref { get; set; }
    }

}




