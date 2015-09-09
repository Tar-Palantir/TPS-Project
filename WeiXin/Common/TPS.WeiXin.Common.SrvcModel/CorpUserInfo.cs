using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel.Enums;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 企业用户信息
    /// </summary>
    public class CorpUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonProperty(PropertyName = "userid")]
        public string UserID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 部门ID列表
        /// </summary>
        [JsonProperty(PropertyName = "department", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DepartmentIDList { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        [JsonProperty(PropertyName = "position", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Position { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Mobile { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty(PropertyName = "gender", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public EnumSex? Sex { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Email { get; set; }

        /// <summary>
        /// 微信号ID
        /// </summary>
        [JsonProperty(PropertyName = "weixinid", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ID { get; set; }

        /// <summary>
        /// 头像ID
        /// </summary>
        [JsonProperty(PropertyName = "avatar_mediaid", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AvatarID { get; set; }

        /// <summary>
        /// 扩展属性，需在通讯录中先添加字段
        /// </summary>
        [JsonProperty(PropertyName = "extattr", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Extention { get; set; }
    }
}