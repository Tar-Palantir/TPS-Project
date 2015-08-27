using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.IFunction.Normal.CustomMenu;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;
using TPS.WeiXin.Common.Model;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class BCustomMenu : ICreate, IDelete, IGetAll, IGetAllInfo
    {
        private const string MenuCreateUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=";

        public OperateStatus Create(Account currentAccount, IList<CustomMenu> menus)
        {
            try
            {
                string url = MenuCreateUrl + AccessTokenHelper.GetAccessToken(currentAccount);

                var menu = new Menu();
                menu.AddButtons(menus);

                var param = JsonConvert.SerializeObject(menu);
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
    }
}