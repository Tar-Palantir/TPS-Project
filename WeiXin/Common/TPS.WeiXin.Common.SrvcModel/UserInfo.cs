using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;

namespace TPS.WeiXin.Common.SrvcModel
{
    public class UserInfo
    {
        [JsonProperty(PropertyName = "subscribe")]
        public int? Subscribe { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string OpenID { get; set; }

        [JsonProperty(PropertyName = "userid")]
        public string UserID { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "sex")]
        public int? Sex { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "headimgurl")]
        public string HeadImgUrl { get; set; }

        [JsonProperty(PropertyName = "subscribe_time",
            ItemConverterType = typeof(UnixDateTimeConverter))]
        public DateTime? SubscribeTime { get; set; }

        [JsonProperty(PropertyName = "unionid")]
        public string UnionID { get; set; }

        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        [JsonProperty(PropertyName = "groupid")]
        public string GroupID { get; set; }

    }
}