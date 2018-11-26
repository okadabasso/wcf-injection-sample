using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Castle.DynamicProxy;
using DryIoc;
using WebServiceSample.Infrastructure.Attributes;
using WebServiceSample.Infrastructure.ComponentManagement;
using NLog;

namespace WebServiceSample.Infrastructure.Aspects
{
    class ServiceOperationAspect : IInterceptor
    {

        private ILogger logger;

        public ServiceOperationAspect()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            logger = GetTargetClassLogger(invocation.TargetType);
            logger.Trace($"method start");
            var aspectAttribute = GetAspectAttribute(invocation.MethodInvocationTarget);
            if (aspectAttribute == null)
            {
                invocation.Proceed();
                return;
            }
            var t = aspectAttribute.Type;
            using (var scope = ComponentManager.GetContainer().OpenScope())
            {
                var service = scope.Resolve(aspectAttribute.Type);
                var method = t.GetMethod(invocation.Method.Name);
                var returnValue = method.Invoke(service, invocation.Arguments);
                invocation.ReturnValue = returnValue;
            }
            logger.Trace($"method end");
        }
        private ILogger GetTargetClassLogger(Type targetClass)
        {
            var logger = LogManager.GetLogger(targetClass.FullName);
            return logger;
        }
        private ServiceOperationAspectAttribute GetAspectAttribute(MethodInfo method)
        {
            // 実装クラスに直接Aspect属性が定義されている場合はそれを採用
            var attribute = method.GetCustomAttribute<ServiceOperationAspectAttribute>(true);
            if(attribute != null)
            {
                return attribute;
            }

            // 実装クラスが実装しているinterfaceにAspect属性が定義されている場合はそれを採用
            foreach (var i in method.DeclaringType.GetInterfaces())
            {
                var interfaceMethod = i.GetMethod(method.Name);
                attribute = interfaceMethod.GetCustomAttribute<ServiceOperationAspectAttribute>(true);
                if (attribute != null)
                {
                    return attribute;
                }
            }
            return null;
        }
    }
}
