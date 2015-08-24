namespace TPS.WeiXin.Extentions.BaseCorpFunction.Exts
{

    public enum EnumWXBizMsgCryptErrorCode
    {
        OK = 0,

        /// <summary>
        /// 签名验证错误
        /// </summary>
        ValidateSignature_Error = -40001,

        /// <summary>
        /// xml解析失败
        /// </summary>
        ParseXml_Error = -40002,

        /// <summary>
        /// sha加密生成签名失败
        /// </summary>
        ComputeSignature_Error = -40003,

        /// <summary>
        /// AESKey 非法
        /// </summary>
        IllegalAesKey = -40004,

        /// <summary>
        /// corpid 校验错误
        /// </summary>
        ValidateCorpid_Error = -40005,

        /// <summary>
        /// AES 加密失败
        /// </summary>
        EncryptAES_Error = -40006,

        /// <summary>
        /// AES 解密失败
        /// </summary>
        DecryptAES_Error = -40007,

        /// <summary>
        /// 解密后得到的buffer非法
        /// </summary>
        IllegalBuffer = -40008,

        /// <summary>
        /// base64加密异常
        /// </summary>
        EncodeBase64_Error = -40009,

        /// <summary>
        /// base64解密异常
        /// </summary>
        DecodeBase64_Error = -40010
    }
}