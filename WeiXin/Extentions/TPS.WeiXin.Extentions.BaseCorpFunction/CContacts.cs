using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseCorpFunction.Common;
using TPS.WeiXin.Extentions.IFunction.Corp.Contacts;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction
{
    public class CContacts : IUserCreate, IUserDelete, IUserUpdate, IUserGetByDepartment, IUserGetByID, IUserInvite
    {
        private const string UserCreateUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";

        private const string UserDeleteUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";

        private const string UserUpdateUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";

        private const string UserGetByIDUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";

        private const string UserGetByDeparmentUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}";

        private const string UserGetDetailByDeparmentUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}";

        private const string UserInviteUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}";


        public OperateStatus Create(Account currentAccount, CorpUserInfo userInfo)
        {
            try
            {
                string url = string.Format(UserCreateUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount));

                var param = JsonConvert.SerializeObject(userInfo);
                var responseResult = HttpHelper.GetResponseResultByPost(url, param, contentType: "application/json");

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
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

        public OperateStatus Delete(Account currentAccount, string userId)
        {
            try
            {
                string url = string.Format(UserDeleteUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount), userId);

                var responseResult = HttpHelper.GetResponseResultByGet(url);

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
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
                    Message = string.Format("删除错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "删除异常，" + ex.Message };
            }
        }

        public OperateStatus Update(Account currentAccount, CorpUserInfo userInfo)
        {
            try
            {
                string url = string.Format(UserUpdateUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount));

                var param = JsonConvert.SerializeObject(userInfo);
                var responseResult = HttpHelper.GetResponseResultByPost(url, param, contentType: "application/json");

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
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

        public OperateStatus GetByID(Account currentAccount, string userId)
        {
            try
            {
                string url = string.Format(UserGetByIDUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount), userId);

                var responseResult = HttpHelper.GetResponseResultByGet(url);

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
                }

                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    var userInfo = JsonConvert.DeserializeObject<CorpUserInfo>(responseResult.ResponseString);

                    return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = JsonConvert.SerializeObject(userInfo) };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("获取错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "获取异常，" + ex.Message };
            }
        }

        public OperateStatus Invite(Account currentAccount, string userId)
        {
            try
            {
                string url = string.Format(UserInviteUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount));

                var responseResult = HttpHelper.GetResponseResultByPost(url, string.Format("{{userid:{0}}}", userId), contentType: "application/json");

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
                }

                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = result.GetValue("type").ToString() };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("邀请错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "邀请异常，" + ex.Message };
            }
        }

        public OperateStatus GetByDepartment(Account currentAccount, string departmentId, bool fetchChild, int enumStatus, bool getDetail)
        {
            try
            {
                var format = getDetail ? UserGetDetailByDeparmentUrlFormat : UserGetByDeparmentUrlFormat;

                string url = string.Format(format, AccessTokenHelper.GetAccessToken(currentAccount), departmentId, fetchChild ? 1 : 0, enumStatus);

                var responseResult = HttpHelper.GetResponseResultByGet(url);

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
                }

                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    var userInfoList = JsonConvert.DeserializeObject<IList<CorpUserInfo>>(result.Value<string>("userlist"));

                    return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = JsonConvert.SerializeObject(userInfoList) };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("获取错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "获取异常，" + ex.Message };
            }
        }
    }
}