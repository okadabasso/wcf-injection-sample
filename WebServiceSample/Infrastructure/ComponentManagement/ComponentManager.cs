using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using NLog;
using WebServiceSample.Infrastructure.Aspects;
using WebServiceSample.Infrastructure.Behavioirs;
using WebServiceSample.DependencyInjection.Attributes;

namespace WebServiceSample.Infrastructure.ComponentManagement
{
    class ComponentManager
    {
        static object o = new object();
        static Container container = null;

        public static void Configure()
        {
            lock (o)
            {
                if (container == null)
                {
                    container = new Container();
                    ConfigureLogger();
                    ConfigureOperationAdapters();
                    ConfigureDomain();
                }
            }


        }

        private static void ConfigureDomain()
        {
            container.RegisterMany(
                typeof(ComponentManager).Assembly.GetTypes().Where(t => t.GetInterfaces().Where(i => i.GetCustomAttribute<ServiceAttribute>() != null).Any()),
                serviceTypeCondition: t => t.GetCustomAttribute<ServiceAttribute>() != null,
                reuse: Reuse.Scoped

                );

        }

        private static void ConfigureOperationAdapters()
        {
            container.Register<ServiceOperationAspect>(ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            // scoped as IFoo
            container.RegisterMany(
                typeof(ComponentManager).Assembly.GetTypes().Where(t => t.GetInterfaces().Where(i => i.GetCustomAttribute<OperationAdapterAttribute>() != null).Any()),
                serviceTypeCondition: t => t.GetCustomAttribute<OperationAdapterAttribute>() != null,
                reuse: Reuse.Scoped

                );
        }

        private static void ConfigureLogger()
        {
            //NLog.LogManager.LoadConfiguration("NLog.config");
            container.Register<ILogger>(
                made: Made.Of(
                        () => LogManager.GetLogger(Arg.Index<string>(0)),
                        request => request.Parent.ImplementationType.FullName
                )
            );
        }

        public static Container GetContainer()
        {
            return container;
        }
    }
}
