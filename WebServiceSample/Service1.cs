using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NLog;
using WebServiceSample.Domain.Services;
using WebServiceSample.Infrastructure.Attributes;
using WebServiceSample.OperationAdapters;

namespace WebServiceSample
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にクラス名 "Service1" を変更できます。
    public class Service1 : IService1
    {
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
        public virtual string GetData2(int value)
        {
            var logger = LogManager.GetLogger(typeof(GetDataService).FullName);
            var service = new GetDataService(logger);

            return service.GetData(value);
        }

        [ServiceOperationAspect(Type = typeof(IGetDataServiceAdatper))]
        public virtual CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }
    }
}
