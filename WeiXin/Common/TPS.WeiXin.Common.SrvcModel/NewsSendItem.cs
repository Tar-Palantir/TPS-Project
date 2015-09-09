using Newtonsoft.Json;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 消息发送项
    /// </summary>
    public class NewsSendItem
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// 封面图片Url
        /// </summary>
        [JsonProperty(PropertyName = "picurl")]
        public string PicUrl { get; set; }
    }
}
