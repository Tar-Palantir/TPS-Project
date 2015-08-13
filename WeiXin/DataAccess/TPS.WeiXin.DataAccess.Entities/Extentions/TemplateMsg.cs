using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TPS.WeiXin.DataAccess.Entities.Extentions
{
    [JsonObject]
    public class TemplateMsg
    {
        public TemplateMsg()
        {
            TopColor = "#FF0000";
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
        public Guid AccountID { get; set; }

        [JsonIgnore]
        public IList<TemplateParameter> Parameters { get; set; }
    }
}
