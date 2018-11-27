﻿using System;
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
}
