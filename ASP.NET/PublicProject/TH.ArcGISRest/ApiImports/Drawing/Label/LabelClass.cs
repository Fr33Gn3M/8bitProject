using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TH.ArcGISRest.ApiImports.JsonConverters;

namespace TH.ArcGISRest.ApiImports.Symbols
{

    [Serializable()]
    [JsonObject()]
    public class LabelClass
    {
        [JsonProperty("labelPlacement")]
        [JsonConverter(typeof(NullableStringEnumConverter))]
        public LabelPlacement? LabelPlacement { get; set; }
        [JsonProperty("labelExpression")]
        public String LabelExpression { get; set; }
        [JsonProperty("useCodedValues")]
        public bool UseCodedValues { get; set; }
        [JsonProperty("symbol")]
        public TextSymbol Symbol { get; set; }
        [JsonProperty("minScale")]
        public Double MinScale { get; set; }
        [JsonProperty("maxScale")]
        public Double MaxScale { get; set; }
    }
}
