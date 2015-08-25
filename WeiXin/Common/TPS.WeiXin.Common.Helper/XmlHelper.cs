using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TPS.WeiXin.Common.Helper
{
    public static class XmlHelper
    {
        public static Dictionary<string, string> ConvertToDictionary(string xmlMsg)
        {
            try
            {
                var sr = new StringReader(xmlMsg);
                var xElement = XElement.Load(sr);
                var nodes = xElement.Elements();
                var result = nodes.ToDictionary(node => node.Name.LocalName, node => node.Value);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("将xml内容转换为对象失败，xml内容【{0}】失败【{1}】", xmlMsg, e));
            }
        }
    }
}
