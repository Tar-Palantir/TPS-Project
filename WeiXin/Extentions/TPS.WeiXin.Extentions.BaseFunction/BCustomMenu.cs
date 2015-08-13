using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.BaseFunction.Exts;
using TPS.WeiXin.Extentions.IFunction.CustomMenu;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class BCustomMenu : ICreate, IDelete, IGetAll, IGetAllInfo
    {
        private const string MenuCreateUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=";
        
        public OperateStatus Create(Guid accountId, IList<CustomMenu> menus)
        {
            try
            {
                string url = MenuCreateUrl + AccessTokenHelper.GetAccessToken(accountId);

                var menu = new Menu();
                menu.AddButtons(menus);

                var param = JsonConvert.SerializeObject(menu);
                var responseResult = HttpHelper.GetResponseResultByPost(url, param);

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
                }

                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                if (result.Value<int>("errcode") == 0)
                {
                    return new OperateStatus { ResultSign = ResultSign.Success };
                }
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送错误，" + result.Value<string>("errmsg") };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送异常，" + ex.Message };
            }
        }
    }
}