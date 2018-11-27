using NLog;
using System;
using System.ServiceModel;
using System.Reflection;
using System.ServiceModel.Dispatcher;
using DryIoc;
using WebServiceSample.Infrastructure.Aspects;
using WebServiceSample.Infrastructure.ComponentManagement;
using WebServiceSample.OperationAdapters;
using WebServiceSample.OperationAdapters.Implementation;
using WebServiceSample.Domain.Services;
using WebServiceSample.Domain.Services.Implementation;

namespace WebServiceSample.Infrastructure.Behavioirs
{
    public class ServiceInstanceProvider : IInstanceProvider
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ServiceInstanceProvider()
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

            // 各所でDIコンテナを共有するためコンテナをSingletonにしておく
            // (ただし注意しないと意図せずServiceLocator化してしまう
            var container = ComponentManager.Current;

            // WCF Service contract
            container.Register<Service1>(ifAlreadyRegistered: IfAlreadyRegistered.Keep);
            container.Intercept<Service1, ServiceOperationAspect>();

            this.ServiceInstance = container.Resolve(type);
            
            // 常駐スレッドの起動
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
