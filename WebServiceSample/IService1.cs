using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Reflection;
using System.Text;
using Castle.Core;
using Castle.DynamicProxy;
using DryIoc;
using ImTools;
using NLog;
using WebServiceSample.OperationAdapters;
using WebServiceSample.Infrastructure.Behavioirs;
using WebServiceSample.Infrastructure.Aspects;
using WebServiceSample.Infrastructure.Attributes;
namespace WebServiceSample
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IService1" を変更できます。
    [SampleContractBehavior]
    [ServiceContract]
    public interface IService1
    {
        [ServiceOperationAspect(Type = typeof(IGetDataServiceAdatper))]
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        string GetData2(int value);

        [ServiceOperationAspect(Type = typeof(IGetDataServiceAdatper))]
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ここにサービス操作を追加します。
    }

    // サービス操作に複合型を追加するには、以下のサンプルに示すようにデータ コントラクトを使用します。
    // プロジェクトに XSD ファイルを追加できます。プロジェクトのビルド後、そこで定義されたデータ型を、名前空間 "WebServiceSample.ContractType" で直接使用できます。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }


}
