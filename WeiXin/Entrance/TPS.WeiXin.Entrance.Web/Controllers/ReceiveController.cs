using System;
using System.Web.Mvc;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Entrance.Web.Models;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Log;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class ReceiveController : BaseServiceController
    {
        public ActionResult Main(string accountID, string signature, string msg_signature, string timestamp, string nonce, string echostr)
        {
            FileLogHelper.WriteInfo(string.Format("accountID:{0}\r\nsignature:{1}\r\ntimestamp:{2}\r\nnonce:{3}\r\nechostr:{4}",
                accountID, signature, timestamp, nonce, echostr));

            OperateStatus status;
            Guid aID;
            if (!Guid.TryParse(accountID, out aID))
            {
                status = new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }
            else
            {
                AccountServiceModel model = new AccountServiceModel();
                var account = model.GetById(aID);
                if (account == null)
                {
                    return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
                }
                if (account.IsCorp)
                {
                    var func = FunctionFactory.GetCorpFunctionInstance<Extentions.IFunction.Corp.Receive.IReceive>();
                    status = func.Main(account, msg_signature, timestamp, nonce, echostr);
                }
                else
                {
                    var func = FunctionFactory.GetFunctionInstance<Extentions.IFunction.Normal.Receive.IReceive>();
                    status = func.Main(account, signature, timestamp, nonce, echostr);
                }
            }
            return new ServiceResult(status);
        }

        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }
    }
}
