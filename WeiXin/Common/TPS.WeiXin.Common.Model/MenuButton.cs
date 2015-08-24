using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TPS.WeiXin.Common.Model
{
    public class MenuButton
    {
        internal Guid ID { set; get; }

        [JsonProperty(PropertyName = "type")]
        internal string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        internal string Name { get; set; }

        [JsonProperty(PropertyName = "url",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string Url { get; set; }

        [JsonProperty(PropertyName = "key",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string Key { get; set; }

        [JsonProperty(PropertyName = "sub_button",
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal IList<MenuButton> SubButton { get; set; }
    }
}
