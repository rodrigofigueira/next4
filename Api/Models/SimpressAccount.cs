using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class SimpressAccount
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
        public List<SimpressAccountValue> value { get; set; }

    }
}
