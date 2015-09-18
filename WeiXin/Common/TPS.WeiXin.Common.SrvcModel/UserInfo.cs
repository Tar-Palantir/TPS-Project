using System;
using System.CodeDom;
using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel.Enums;
using Zeus.Common.Helper.Json;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 关注状态，0、未关注 
        /// </summary>
        [JsonProperty(PropertyName = "subscribe")]
        public int? Subscribe { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [JsonProperty(PropertyName = "openid")]
        public string OpenID { get; set; }

        /// <summary>
        /// 用户ID,企业号用户才有该字段
        /// </summary>
        [JsonProperty(PropertyName = "userid")]
        public string UserID { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public EnumSex? Sex { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [JsonProperty(PropertyName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// 头像图片Url
        /// </summary>
        [JsonProperty(PropertyName = "headimgurl")]
        public string HeadImgUrl { get; set; }

        /// <summary>
        /// 关注事件
        /// </summary>
        [JsonProperty(PropertyName = "subscribe_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? SubscribeTime { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        [JsonProperty(PropertyName = "unionid")]
        public string UnionID { get; set; }

        /// <summary>
        /// 公众号运营者对粉丝的备注
        /// </summary>
        [JsonProperty(PropertyName = "remark")]
        public string Remark { get; set; }

        /// <summary>
        /// 分组ID
        /// </summary>
        [JsonProperty(PropertyName = "groupid")]
        public string GroupID { get; set; }

    }
}