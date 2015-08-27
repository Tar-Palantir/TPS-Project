using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.IFunction.Normal.UserManage;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class BUserManage : IGetUserBaseInfo, IGroup, ISetRemark
    {
        private const string GetUserInfoUrlFormat =
            "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
        private const string CreateGroupUrlFormat =
            "https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}";
        private const string MoveUserUrlFormat =
            "https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}";


        public UserInfo GetByOpenID(Account currentAccount, string openID)
        {
            var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);

            var url = string.Format(GetUserInfoUrlFormat, accessToken, openID);
            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserInfo>(responseResult.ResponseString);
        }

        public OperateStatus CreateGroup(Account currentAccount, string name)
        {
            try
            {
                var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);
                var postParams = new { group = new { name = name } };
                var postParamsStr = JsonConvert.SerializeObject(postParams);

                var url = string.Format(CreateGroupUrlFormat, accessToken);
                var responseResult = HttpHelper.GetResponseResultByPost(url, postParamsStr,
                    contentType: "application/json");
                if (responseResult.Status != ResponseStatus.Success)
                {
                    return null;
                }
                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    return new OperateStatus { ResultSign = ResultSign.Success };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("创建错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };
            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "创建异常，" + ex.Message };
            }
        }

        public OperateStatus MoveUser(Account currentAccount, string openID, string groupID)
        {
            try
            {
                var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);
                var postParams = new { openid = openID, to_groupid = groupID };
                var postParamsStr = JsonConvert.SerializeObject(postParams);

                var url = string.Format(MoveUserUrlFormat, accessToken);
                var responseResult = HttpHelper.GetResponseResultByPost(url, postParamsStr, contentType: "application/json");
                if (responseResult.Status != ResponseStatus.Success)
                {
                    return null;
                }
                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    return new OperateStatus { ResultSign = ResultSign.Success };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("移动错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };
            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "移动异常，" + ex.Message };
            }
        }
    }
}