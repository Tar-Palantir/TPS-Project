using Newtonsoft.Json;

namespace TPS.WeiXin.Common.SrvcModel
{
    public class NewsSendItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "picurl")]
        public string PicUrl { get; set; }
    }
}
