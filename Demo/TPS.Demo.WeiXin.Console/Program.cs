using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using Zeus.Common.Helper.Web;

namespace TPS.Demo.WeiXin.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string urlFormat = "http://localhost:9553/SendMsg/TemplateMsg?accountId={0}&TemplateID={1}&ToUser={2}&MsgType={3}&Url={4}";
            string url = string.Format(urlFormat, "3E9C8435-D11D-4316-B387-44B359076267",
                "Ieoxusdbx0KcuasE1_HlBpQ57W7SsrRXnLBJJhtdWT8", "oJ6vjs60OafkWVH4lHhAAbyim9ls",
                (int)EnumMsgType.Success, "http://www.baidu.com");

            IList<TemplateParameter> paramList = new List<TemplateParameter>();
            paramList.Add(new TemplateParameter { Name = "first", Value = "微信绑定" });
            paramList.Add(new TemplateParameter { Name = "keyword1", Value = "13000000000" });
            paramList.Add(new TemplateParameter { Name = "keyword2", Value = "您已成功绑定" });
            paramList.Add(new TemplateParameter { Name = "remark", Value = "欢迎使用，我们竭诚为您服务。" });

            string postParamStr = JsonConvert.SerializeObject(paramList);

            var responseResult = HttpHelper.GetResponseResultByPost(url, postParamStr,contentType: "application/json");
            if (responseResult.Status == ResponseStatus.Success)
            {
                System.Console.WriteLine("成功");
            }
            else
            {
                System.Console.WriteLine(responseResult.ResponseString);
                System.Console.WriteLine(responseResult.ExceptionMessages);
            }

            System.Console.ReadKey();
        }
    }
}
