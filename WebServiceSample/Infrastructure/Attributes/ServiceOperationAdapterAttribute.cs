using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceSample.Infrastructure.Behavioirs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple =false,Inherited =true)]
    public class OperationAdapterAttribute : Attribute
    {
    }
}
