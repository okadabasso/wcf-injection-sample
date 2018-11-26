using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ServiceClient.ServiceReference1;
namespace ServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceProxySample();
        }

        private static void Benckmark()
        {
            var benchmarks = typeof(Program).Assembly.GetTypes()
                .Where(t => t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                .Any(m => m.GetCustomAttributes(typeof(BenchmarkAttribute), false).Any()))
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name)
                .ToArray();
            var switcher = new BenchmarkSwitcher(benchmarks);
            switcher.Run(new string[] { "0" });

        }
        private static void ServiceProxySample()
        {
            var client = new ServiceReference1.Service1Client();
            var result = client.GetData(10);
            Console.WriteLine($"Service1#GetData result={result}");


            var result2 = client.GetData(20);
            Console.WriteLine($"Service1#GetData result={result2}");

            var result3 = client.GetData2(30);
            Console.WriteLine($"Service1#GetData2 result={result3}");

            var composite = new CompositeType();
            composite.BoolValue = true;
            composite.StringValue = "new composite object";
            var result4 = client.GetDataUsingDataContract(composite);

            Console.WriteLine($"Service1#GetDataUsingDataContract result={result4.StringValue}");

            Console.ReadLine();
        }
    }
}
