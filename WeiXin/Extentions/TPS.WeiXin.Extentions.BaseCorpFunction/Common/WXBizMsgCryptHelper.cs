using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseCorpFunction.Exts;
using Zeus.Common.Helper.Cryptography;

namespace TPS.WeiXin.Extentions.BaseCorpFunction.Common
{
    public class WXBizMsgCryptHelper
    {
        //验证URL
        // @param sMsgSignature: 签名串，对应URL参数的msg_signature
        // @param sTimeStamp: 时间戳，对应URL参数的timestamp
        // @param sNonce: 随机串，对应URL参数的nonce
        // @param sEchoStr: 随机串，对应URL参数的echostr
        // @param sReplyEchoStr: 解密之后的echostr，当return返回0时有效
        // @return：成功0，失败返回对应的错误码
        public EnumWXBizMsgCryptErrorCode VerifyURL(Account account, string sMsgSignature, string sTimeStamp, string sNonce, string sEchoStr, out string sReplyEchoStr)
        {
            sReplyEchoStr = string.Empty;
            if (account.EncodingAESKey.Length != 43)
            {
                return EnumWXBizMsgCryptErrorCode.IllegalAesKey;
            }
            var ret = VerifySignature(account.Token, sTimeStamp, sNonce, sEchoStr, sMsgSignature);
            if (0 != ret)
            {
                return ret;
            }
            string cpid;
            try
            {
                sReplyEchoStr = WXCryptHelper.AES_Decrypt(sEchoStr, account.EncodingAESKey, out cpid); //m_sCorpID);
            }
            catch (Exception)
            {
                sReplyEchoStr = "";
                return EnumWXBizMsgCryptErrorCode.DecryptAES_Error;
            }
            if (cpid != account.AppID)
            {
                sReplyEchoStr = "";
                return EnumWXBizMsgCryptErrorCode.ValidateCorpid_Error;
            }
            return EnumWXBizMsgCryptErrorCode.OK;
        }

        // 检验消息的真实性，并且获取解密后的明文
        // @param sMsgSignature: 签名串，对应URL参数的msg_signature
        // @param sTimeStamp: 时间戳，对应URL参数的timestamp
        // @param sNonce: 随机串，对应URL参数的nonce
        // @param sPostData: 密文，对应POST请求的数据
        // @param sMsg: 解密后的原文，当return返回0时有效
        // @return: 成功0，失败返回对应的错误码
        public EnumWXBizMsgCryptErrorCode DecryptMsg(Account account, string sMsgSignature, string sTimeStamp, string sNonce, string sPostData, out string sMsg)
        {
            sMsg = string.Empty;
            if (account.EncodingAESKey.Length != 43)
            {
                return EnumWXBizMsgCryptErrorCode.IllegalAesKey;
            }
            XmlDocument doc = new XmlDocument();
            string sEncryptMsg;
            try
            {
                doc.LoadXml(sPostData);
                var root = doc.FirstChild;
                sEncryptMsg = root["Encrypt"].InnerText;
            }
            catch (Exception)
            {
                return EnumWXBizMsgCryptErrorCode.ParseXml_Error;
            }
            //verify signature
            var ret = VerifySignature(account.Token, sTimeStamp, sNonce, sEncryptMsg, sMsgSignature);
            if (ret != EnumWXBizMsgCryptErrorCode.OK)
                return ret;
            //decrypt
            string cpid;
            try
            {
                sMsg = WXCryptHelper.AES_Decrypt(sEncryptMsg, account.EncodingAESKey, out cpid);
            }
            catch (FormatException)
            {
                return EnumWXBizMsgCryptErrorCode.DecodeBase64_Error;
            }
            catch (Exception)
            {
                return EnumWXBizMsgCryptErrorCode.DecryptAES_Error;
            }
            return cpid != account.AppID ? EnumWXBizMsgCryptErrorCode.ValidateCorpid_Error : EnumWXBizMsgCryptErrorCode.OK;
        }

        //将企业号回复用户的消息加密打包
        // @param sReplyMsg: 企业号待回复用户的消息，xml格式的字符串
        // @param sTimeStamp: 时间戳，可以自己生成，也可以用URL参数的timestamp
        // @param sNonce: 随机串，可以自己生成，也可以用URL参数的nonce
        // @param sEncryptMsg: 加密后的可以直接回复用户的密文，包括msg_signature, timestamp, nonce, encrypt的xml格式的字符串,
        //						当return返回0时有效
        // return：成功0，失败返回对应的错误码
        public EnumWXBizMsgCryptErrorCode EncryptMsg(Account account, string sReplyMsg, string sTimeStamp, string sNonce, out string sEncryptMsg)
        {
            sEncryptMsg = "";
            if (account.EncodingAESKey.Length != 43)
            {
                return EnumWXBizMsgCryptErrorCode.IllegalAesKey;
            }
            string raw;
            try
            {
                raw = WXCryptHelper.AES_Encrypt(sReplyMsg, account.EncodingAESKey, account.AppID);
            }
            catch (Exception)
            {
                return EnumWXBizMsgCryptErrorCode.EncryptAES_Error;
            }
            string MsgSigature;
            var ret = GenarateSinature(account.Token, sTimeStamp, sNonce, raw, out MsgSigature);
            if (EnumWXBizMsgCryptErrorCode.OK != ret)
            {
                return ret;
            }
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("<xml><Encrypt>)<![CDATA[{0}]]></Encrypt>", raw);
            strBuilder.AppendFormat("<MsgSignature><![CDATA[{0}]]></MsgSignature>", MsgSigature);
            strBuilder.AppendFormat("<TimeStamp><![CDATA[{0}]]></TimeStamp>", sTimeStamp);
            strBuilder.AppendFormat("<Nonce><![CDATA[{0}]]></Nonce></xml>", sNonce);

            sEncryptMsg = strBuilder.ToString();
            return EnumWXBizMsgCryptErrorCode.OK;
        }


        private static EnumWXBizMsgCryptErrorCode VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
        {
            string hash;
            var ret = GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, out hash);
            if (ret != (int)EnumWXBizMsgCryptErrorCode.OK)
                return ret;

            return hash == sSigture
                        ? EnumWXBizMsgCryptErrorCode.OK
                        : EnumWXBizMsgCryptErrorCode.ValidateSignature_Error;

        }

        public static EnumWXBizMsgCryptErrorCode GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, out string sMsgSignature)
        {
            var rawList = new List<string> { sTimeStamp, sToken, sNonce, sMsgEncrypt };
            rawList.Sort();
            var rawStr = rawList.Aggregate(string.Empty, (s, c) => s + c);
            HashCryptography.Sha1Encrypt(rawStr, EncryptResultType.Hexadecimal, "ascii");

            string hash;
            try
            {
                hash = HashCryptography.Sha1Encrypt(rawStr, EncryptResultType.Hexadecimal, "ascii");
                hash = hash.ToLower();
            }
            catch (Exception)
            {
                sMsgSignature = string.Empty;
                return EnumWXBizMsgCryptErrorCode.ComputeSignature_Error;
            }
            sMsgSignature = hash;
            return EnumWXBizMsgCryptErrorCode.OK;
        }
    }
}
