using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using WebServiceSample.DependencyInjection.Attributes;
namespace WebServiceSample.Domain.Services.Implementation
{
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

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            logger.Trace("disposed");
        }
        #endregion
    }
}
