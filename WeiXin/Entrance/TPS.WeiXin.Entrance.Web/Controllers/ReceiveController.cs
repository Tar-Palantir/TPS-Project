using System;
using System.Web.Mvc;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Extentions.IFunction.Receive;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Log;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class ReceiveController : BaseServiceController
    {
        public ActionResult Main(string accountID, string signature, string timestamp, string nonce, string echostr)
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
                var func = FunctionFactory.GetFunctionInstance<IReceive>();
                status = func.Main(aID, signature, timestamp, nonce, echostr);
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
