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

}
