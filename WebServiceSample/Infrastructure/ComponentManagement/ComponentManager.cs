using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using NLog;
using WebServiceSample;
using WebServiceSample.Infrastructure.Aspects;
using WebServiceSample.Infrastructure.Behavioirs;
using WebServiceSample.DependencyInjection.Attributes;
using WebServiceSample.Domain.Services;
using WebServiceSample.OperationAdapters;
using WebServiceSample.OperationAdapters.Implementation;
using WebServiceSample.Domain.Services.Implementation;

namespace WebServiceSample.Infrastructure.ComponentManagement
{
    class ComponentManager
    {
        static readonly object lockObject = new object();
        static Container container = null;

        public static void Configure()
        {
            container = new Container();
            // logger
            container.Register<ILogger>(
                made: Made.Of(
                        () => LogManager.GetLogger(Arg.Index<string>(0)),
                        request => request.Parent.ImplementationType.FullName
                )
            );
            // operation adapters
            container.Register<ServiceOperationAspect>(ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            container.Register<IGetDataServiceAdatper, GetDataServiceAdapter>(reuse: Reuse.Scoped);
            // domain
            container.Register<IGetDataService, GetDataService>(reuse: Reuse.Scoped);

            // WCF Service contract
            container.Register<Service1>();
            container.Intercept<Service1, ServiceOperationAspect>();
        }

        public static Container Current
        {
            get
            {
                lock (lockObject)
                {
                    if (container == null)
                    {
                        Configure();
                    }
                }
                return container;
            }
        }
    }
}
