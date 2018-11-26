using NLog;
using System;
using System.ServiceModel.Description;
using WebServiceSample.Infrastructure.Behavioirs;

namespace WebServiceSample.Infrastructure.Behavioirs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = true)]
    public class SampleContractBehaviorAttribute : Attribute, IContractBehavior, IContractBehaviorAttribute
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = new SampleInstanceProvider();
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        public Type TargetContract
        {
            get { return null; }
        }
    }
}
