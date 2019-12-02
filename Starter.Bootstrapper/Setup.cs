using System;

using Unity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Starter.Data.Consumers;
using Starter.Data.Entities;
using Starter.Data.Services;
using Starter.Data.ViewModels;
using Starter.Data.Repositories;

using Starter.Framework.Clients;
using Starter.Framework.Loggers;
using Starter.Framework.Entities;

using Starter.MessageBroker.Azure;
using Starter.Repository.Repositories;

namespace Starter.Bootstrapper
{
    /// <summary>
    /// Sets up the dependency resolution for the project
    /// </summary>
    public static class Setup
    {
        /// <summary>
        /// Sets the dependency resolution for the web project
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider Web(IServiceCollection services)
        {
            // Register all the dependencies
            Bootstrap();

            return IocWrapper.Instance.Container.Resolve<IServiceProvider>();
        }

        /// <summary>
        /// Provides means to registry different service implementations
        /// based on the setup type
        /// </summary>
        public static void Bootstrap(SetupType setupType = SetupType.Debug)
        {
            var container = new UnityContainer();

            switch (setupType)
            {
                case SetupType.Release:
                    container.RegisterType<ISettings, Settings>();

                    break;
                case SetupType.Debug:
                    container.RegisterType<ISettings, SettingsDev>();

                    break;
                case SetupType.Test:
                    container.RegisterType<ISettings, SettingsTest>();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(setupType), setupType, null);
            }
            
            container.RegisterType<IApiClient, ApiClient>();
            container.RegisterType<ILogger, ApplicationInsightsLogger>();

            container.RegisterType<ICatRepository, CatRepository>();
            container.RegisterType<IMessageBroker<Cat>, AzureMessageBroker<Cat>>();
            container.RegisterType<IMessageBrokerConsumer, MessageBrokerConsumer>();
            container.RegisterType<ICatService, CatService>();
            container.RegisterType<IMainViewModel, MainViewModel>();

            IocWrapper.Instance = new IocWrapper(container);
        }
    }
}