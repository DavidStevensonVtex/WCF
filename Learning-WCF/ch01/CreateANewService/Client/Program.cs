using Client.localhost;
using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HelloIndigoServiceClient proxy = new HelloIndigoServiceClient();
            string s = proxy.HelloIndigo();
            Console.WriteLine(s);
            Console.WriteLine("Press <Enter> to exit program.");
            Console.ReadLine();
        }
    }
}
