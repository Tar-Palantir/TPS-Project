using Newtonsoft.Json;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 图文消息发送项
    /// </summary>
    public class ArticleSendItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }

        /// <summary>
        /// 缩略图图片ID
        /// </summary>
        [JsonProperty(PropertyName = "thumb_media_id")]
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        /// <summary>
        /// 内容原文Url
        /// </summary>
        [JsonProperty(PropertyName = "content_source_url")]
        public string ContentSourceUrl { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty(PropertyName = "digest")]
        public string Digest { get; set; }

        /// <summary>
        /// 内容中是否展示封面图片
        /// </summary>
        [JsonProperty(PropertyName = "show_cover_pic")]
        public bool ShouldShowCoverPic { get; set; }
    }
}
