using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace TPS.WeiXin.Common.Helper
{
    public static class FunctionFactory
    {
        private static IUnityContainer container;
        private static IUnityContainer corpContainer;

        #region ctor静态构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static FunctionFactory()
        {
            RefreshContainer();
            RefreshCorpContainer();
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
        public static void RefreshCorpContainer()
        {
            corpContainer = new UnityContainer();
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("funcCorpUnity");
            if (section == null)
                throw new ConfigurationErrorsException("funcCorpUnity");
            section.Configure(corpContainer);
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

        /// <summary>
        /// 获取方法实例
        /// </summary>
        /// <typeparam name="IFunction">方法接口</typeparam>
        /// <returns>方法实例</returns>
        public static IFunction GetCorpFunctionInstance<IFunction>()
        {
            return corpContainer.Resolve<IFunction>();
        }
    }
}