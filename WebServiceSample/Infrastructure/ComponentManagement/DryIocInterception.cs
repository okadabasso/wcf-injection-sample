using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using ImTools;
using DryIoc;
namespace WebServiceSample.Infrastructure.ComponentManagement
{
    public static class DryIocInterception
    {
        static readonly DefaultProxyBuilder ProxyBuilder = new DefaultProxyBuilder();
        public static void Intercept<TService, TInterceptor>(this IRegistrator registrator, object serviceKey = null)
           where TInterceptor : class, IInterceptor
        {
            var serviceType = typeof(TService);

            Type proxyType;
            if (serviceType.IsInterface())
            {
                proxyType = ProxyBuilder.CreateInterfaceProxyTypeWithTargetInterface(
                    serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            }
            else if (serviceType.IsClass())
            {
                proxyType = ProxyBuilder.CreateClassProxyType(
                    serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            }
            else
            {
                throw new ArgumentException(
                    $"Intercepted service type {serviceType} is not a supported, cause it is nor a class nor an interface");
            }

            var decoratorSetup = serviceKey == null
                ? Setup.DecoratorWith(useDecorateeReuse: true)
                : Setup.DecoratorWith(r => serviceKey.Equals(r.ServiceKey), useDecorateeReuse: true);

            registrator.Register(serviceType, proxyType,
                made: Made.Of(type => type.PublicConstructors().SingleOrDefault(c => c.GetParameters().Length != 0),
                    Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[]))),
                setup: decoratorSetup);
        }
        public static void Intercept<TInterceptor>(this IRegistrator registrator, Type serviceType, object serviceKey = null)
           where TInterceptor : class, IInterceptor
        {
            Type proxyType;
            if (serviceType.IsInterface())
            {
                proxyType = ProxyBuilder.CreateInterfaceProxyTypeWithTargetInterface(
                    serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            }
            else if (serviceType.IsClass())
            {
                proxyType = ProxyBuilder.CreateClassProxyType(
                    serviceType, ArrayTools.Empty<Type>(), ProxyGenerationOptions.Default);
            }
            else
            {
                throw new ArgumentException(
                    $"Intercepted service type {serviceType} is not a supported, cause it is nor a class nor an interface");
            }

            var decoratorSetup = serviceKey == null
                ? Setup.DecoratorWith(useDecorateeReuse: true)
                : Setup.DecoratorWith(r => serviceKey.Equals(r.ServiceKey), useDecorateeReuse: true);

            registrator.Register(serviceType, proxyType,
                made: Made.Of(type => type.PublicConstructors().SingleOrDefault(c => c.GetParameters().Length != 0),
                    Parameters.Of.Type<IInterceptor[]>(typeof(TInterceptor[]))),
                setup: decoratorSetup);
        }
        public static void Intercept<TInterceptor>(this IRegistrator registrator, Func<Type, bool> predicate)
           where TInterceptor : class, IInterceptor
        {
            foreach (var registration in registrator.GetServiceRegistrations().Where(r => predicate(r.ServiceType)))
            {
                registrator.Intercept<TInterceptor>(registration.ServiceType);
            }
        }
    }
}
