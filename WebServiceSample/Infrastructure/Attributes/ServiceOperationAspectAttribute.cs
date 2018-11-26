using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceSample.Infrastructure.Attributes
{
    public class ServiceOperationAspectAttribute : Attribute
    {
        public Type Type { get; set; }
    }
}
