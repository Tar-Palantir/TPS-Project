using Newtonsoft.Json;

namespace TPS.WeiXin.Common.Model
{
    public class ArticleReplyItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }

        [JsonProperty(PropertyName = "description")]
        public string Description { set; get; }

        [JsonProperty(PropertyName = "picurl")]
        public string PicUrl { set; get; }

        [JsonProperty(PropertyName = "url")]
        public string Url { set; get; }
    }
}
