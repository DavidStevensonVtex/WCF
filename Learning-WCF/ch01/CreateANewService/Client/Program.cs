using System;
using System.ServiceModel;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EndpointAddress ep = new EndpointAddress("http://localhost:8000/HelloIndigo/HelloIndigoService");
            IHelloIndigoService proxy = ChannelFactory<IHelloIndigoService>.CreateChannel(new BasicHttpBinding(), ep);
            string s = proxy.HelloIndigo();
            Console.WriteLine(s);
            Console.WriteLine("Press <Enter> to exit program.");
            Console.ReadLine();
        }
    }
}
