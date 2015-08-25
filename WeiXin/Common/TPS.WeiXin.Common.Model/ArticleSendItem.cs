using Newtonsoft.Json;

namespace TPS.WeiXin.Common.Model
{
    public class ArticleSendItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }

        [JsonProperty(PropertyName = "thumb_media_id")]
        public string ThumbMediaId { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "content_source_url")]
        public string ContentSourceUrl { get; set; }

        [JsonProperty(PropertyName = "digest")]
        public string Digest { get; set; }

        [JsonProperty(PropertyName = "show_cover_pic")]
        public bool ShouldShowCoverPic { get; set; }
    }
}
