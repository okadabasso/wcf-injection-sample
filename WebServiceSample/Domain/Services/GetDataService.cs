using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WebServiceSample.DependencyInjection.Attributes;

namespace WebServiceSample.Domain.Services
{
    [Service]
    public interface IGetDataService
    {
        string GetData(int value);

        CompositeType GetDataUsingDataContract(CompositeType composite);
    }
    public class GetDataService : IGetDataService, IDisposable
    {
        protected readonly ILogger logger;
        public GetDataService(ILogger logger)
        {
            this.logger = logger;
            logger.Trace("construct");
        }


        public virtual string GetData(int value)
        {
            logger.Trace("execute GetData");
            return string.Format("override service by GetDataService: {0}", value);
        }

        public virtual CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            logger.Trace("execute GetDataUsingDataContract");
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public void Dispose()
        {
            logger.Trace("dispose");
        }
    }
}
