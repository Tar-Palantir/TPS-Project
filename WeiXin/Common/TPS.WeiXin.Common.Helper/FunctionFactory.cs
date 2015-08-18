using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TPS.WeiXin.Common.Helper
{
    public static class FunctionFactory
    {
        private static IUnityContainer container;

        #region ctor静态构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static FunctionFactory()
        {
            RefreshContainer();
        }
        #endregion


        public static void RefreshContainer()
        {
            container = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("funcUnity");
            if (section == null)
                throw new ConfigurationErrorsException("funcUnity");
            section.Configure(container);
        }


        /// <summary>
        /// 获取方法实例
        /// </summary>
        /// <typeparam name="IFunction">方法接口</typeparam>
        /// <returns>方法实例</returns>
        public static IFunction GetFunctionInstance<IFunction>()
        {
            return container.Resolve<IFunction>();
        }
    }
}