using NLog;
using System;
using System.ServiceModel;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using DryIoc;
using WebServiceSample.Infrastructure.Aspects;
using WebServiceSample.Infrastructure.ComponentManagement;

namespace WebServiceSample.Infrastructure.Behavioirs
{
    public class SampleInstanceProvider : IInstanceProvider
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public SampleInstanceProvider()
        {
        }

        /// <summary>
        /// 実行されるサービスのインスタンス
        /// </summary>
        public object ServiceInstance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceContext"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public object GetInstance(System.ServiceModel.InstanceContext instanceContext, System.ServiceModel.Channels.Message message)
        {
            Type type = instanceContext.Host.Description.ServiceType;
            if (type == null)
            {
                throw new InvalidOperationException();
            }
            ComponentManager.Configure();
            var container = ComponentManager.GetContainer();
            container.Register(type, ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            container.Intercept<ServiceOperationAspect>(type);

            this.ServiceInstance = container.Resolve(type);
            RunWorkersThreads();

            return this.ServiceInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public object GetInstance(System.ServiceModel.InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(System.ServiceModel.InstanceContext instanceContext, object instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// ここで稼働させる常駐スレッドはアプリケーションプールのリサイクル
        /// を必ず考慮すること。
        /// リサイクルが発生するとまずいような処理の場合はここに実装してはいけない。
        /// </summary>
        private void RunWorkersThreads()
        {
        }
    }
}
