using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;

namespace ServiceClient.Benckmark
{
[Config(typeof(BenchmarkConfig))]
public class WebServiceBenchmark
{
    [Setup]
    public void SetUp()
    {
    }
    [Benchmark]
    public string GetData()
    {
        var client = new ServiceReference1.Service1Client();
        var result = client.GetData(10);

        return result;
    }
}
}
