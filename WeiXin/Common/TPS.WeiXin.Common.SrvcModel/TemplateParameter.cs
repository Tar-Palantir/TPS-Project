namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 模板参数
    /// </summary>
    public class TemplateParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TemplateParameter()
        {
            Color = "#173177";
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 参数显示颜色
        /// </summary>
        public string Color { get; set; }
    }
}
