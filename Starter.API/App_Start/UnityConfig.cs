using System.Web.Http;

using Unity.WebApi;

using Starter.Bootstrapper;

namespace Starter.API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
#if DEBUG
            Setup.Bootstrap();
#else
            Setup.Bootstrap(SetupType.Release);
#endif

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(IocWrapper.Instance.Container);
        }
    }
}