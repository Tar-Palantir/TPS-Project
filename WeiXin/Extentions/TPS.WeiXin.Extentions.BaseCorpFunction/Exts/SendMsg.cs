using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.SrvcModel;

namespace TPS.WeiXin.Extentions.BaseCorpFunction.Exts
{
    public class SendMsg
    {
        private SendMsg(int agentID, SendMessageTarget target)
        {
            ToUser = target.IsSendAll ? "@all" : target.UserIDList.Aggregate(string.Empty, (s, c) => s + c + "|");
            ToParty = target.DepartmentIDList.Aggregate(string.Empty, (s, c) => s + c + "|");
            ToTag = target.TagIDLIst.Aggregate(string.Empty, (s, c) => s + c + "|");

            AgentID = agentID;
        }

        public SendMsg(int agentID, SendMessageTarget target, string message)
            : this(agentID, target)
        {
            MsgType = "text";

            NewsList = null;
            ArticleList = null;
            Text = message;
            Voice = null;
            Image = null;
            File = null;
            Video = null;

            Safe = 0;
        }
        public SendMsg(int agentID, SendMessageTarget target, IList<ArticleSendItem> message)
            : this(agentID, target)
        {
            MsgType = "mpnews";

            NewsList = null;
            ArticleList = message;
            Text = null;
            Voice = null;
            Image = null;
            File = null;
            Video = null;

            Safe = 0;
        }
        public SendMsg(int agentID, SendMessageTarget target, IList<NewsSendItem> message)
            : this(agentID, target)
        {
            MsgType = "news";

            NewsList = message;
            ArticleList = null;
            Text = null;
            Voice = null;
            Image = null;
            File = null;
            Video = null;

            Safe = null;
        }

        [JsonProperty(PropertyName = "touser")]
        public string ToUser { get; set; }

        [JsonProperty(PropertyName = "toparty")]
        public string ToParty { get; set; }

        [JsonProperty(PropertyName = "totag")]
        public string ToTag { get; set; }

        [JsonProperty(PropertyName = "msgtype")]
        public string MsgType { get; set; }

        [JsonProperty(PropertyName = "rext", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "voice", NullValueHandling = NullValueHandling.Ignore)]
        public string Voice { get; set; }

        [JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "video", NullValueHandling = NullValueHandling.Ignore)]
        public string Video { get; set; }

        [JsonProperty(PropertyName = "file", NullValueHandling = NullValueHandling.Ignore)]
        public string File { get; set; }

        [JsonProperty(PropertyName = "news", NullValueHandling = NullValueHandling.Ignore)]
        public Object News { get { return NewsList == null ? null : new { articles = NewsList }; } }

        [JsonProperty(PropertyName = "mpnews", NullValueHandling = NullValueHandling.Ignore)]
        public Object MPNews { get { return ArticleList == null ? null : new { articles = ArticleList }; } }

        [JsonProperty(PropertyName = "agentid", NullValueHandling = NullValueHandling.Ignore)]
        public int AgentID { get; set; }

        [JsonProperty(PropertyName = "safe", NullValueHandling = NullValueHandling.Ignore)]
        public int? Safe { get; set; }

        [JsonIgnore]
        public IList<ArticleSendItem> ArticleList { get; set; }

        [JsonIgnore]
        public IList<NewsSendItem> NewsList { get; set; }



    }
}
