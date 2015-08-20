using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.Model.Enums;

namespace TPS.WeiXin.Extentions.BaseFunction.Exts
{
    [JsonObject]
    public class TemplateMsg
    {
        public TemplateMsg() { }

        public TemplateMsg(TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            OpenID = templateMsgParams.ToUser;
            TemplateID = templateMsgParams.TemplateID;
            Url = templateMsgParams.Url;
            TopColor = GetTopColor(templateMsgParams.MsgType);
            Parameters = parameters;
        }

        [JsonProperty(PropertyName = "touser")]
        public string OpenID { get; set; }

        [JsonProperty(PropertyName = "template_id")]
        public string TemplateID { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "topcolor")]
        public string TopColor { get; set; }

        [JsonProperty(PropertyName = "data")]
        public JObject Data
        {
            get
            {
                if (Parameters == null)
                {
                    return null;
                }

                var data = Parameters.Select(p =>
                    new JProperty(p.Name,
                        JObject.FromObject(new
                        {
                            value = p.Value,
                            color = p.Color
                        }))
                ).ToList();

                return new JObject(data);
            }
        }
        
        [JsonIgnore]
        public IList<TemplateParameter> Parameters { get; set; }

        private string GetTopColor(EnumMsgType msgType)
        {
            switch (msgType)
            {
                case EnumMsgType.Success:
                    return "#33CC33";
                case EnumMsgType.Fail:
                    return "#CC3333";
                case EnumMsgType.Warning:
                    return "#FFCC66";
                default:
                    return "#FF0000";
            }
        }
    }
}
