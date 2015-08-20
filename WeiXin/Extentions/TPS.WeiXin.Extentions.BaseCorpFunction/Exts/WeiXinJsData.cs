namespace TPS.WeiXin.Extentions.BaseCorpFunction.Exts
{
    /// <summary>
    /// 微信JS请求数据
    /// </summary>
    public class WeiXinJsData
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
    }
}
