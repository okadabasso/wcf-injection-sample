using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WebServiceSample.Infrastructure.Behavioirs;
using WebServiceSample.Domain.Services;

namespace WebServiceSample.OperationAdapters
{
    [OperationAdapter]
    public interface IGetDataServiceAdatper
    {
        string GetData(int value);
        CompositeType GetDataUsingDataContract(CompositeType composite);
    }
    public class GetDataServiceAdapter : IGetDataServiceAdatper
    {
        private readonly NLog.ILogger logger;
        private readonly IGetDataService getDataService;
        public GetDataServiceAdapter(NLog.ILogger logger, IGetDataService getDataService)
        {
            this.logger = logger;
            this.getDataService = getDataService;
        }
        public virtual string GetData(int value)
        {
            logger.Debug($"GetData invoked with parameter {value}");
            return getDataService.GetData(value);
        }

        public virtual CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            logger.Debug($"GetDataUsingDataContract invoked with parameter ");
            return getDataService.GetDataUsingDataContract(composite);
        }
    }

}
