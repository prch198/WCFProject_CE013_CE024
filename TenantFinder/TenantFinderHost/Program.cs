using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TenantFinder;

namespace TenantFinderHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Type t = typeof(HouseService);
            Uri http = new Uri("http://localhost:8001/TenantFinder");
            ServiceHost host = new ServiceHost(t, http);
            host.Open();
            Console.WriteLine("Published");
            Console.ReadLine();
            host.Close();
        }
    }
}
