using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NLog;
using WebServiceSample.Domain.Services;
using WebServiceSample.Domain.Services.Implementation;
using WebServiceSample.Infrastructure.Attributes;
using WebServiceSample.OperationAdapters;

namespace WebServiceSample
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にクラス名 "Service1" を変更できます。
    public class Service1 : IService1
    {
        static ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// implement by ServiceOperationAdapter
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual string GetData(int value)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// implement direct
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetData2(int value)
        {
            try
            {
                logger.Trace(MethodInfo.GetCurrentMethod().Name + " method start");
                using (var service = new GetDataService(LogManager.GetLogger(typeof(GetDataService).FullName)))
                {
                    return service.GetData(value);
                }
            }
            finally
            {
                logger.Trace(MethodInfo.GetCurrentMethod().Name + " method end");
            }

        }

        public virtual CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }
    }
}
