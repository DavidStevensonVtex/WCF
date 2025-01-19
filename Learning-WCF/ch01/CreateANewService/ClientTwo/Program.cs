using System;

namespace ClientTwo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (HelloIndigoServiceClient proxy = new HelloIndigoServiceClient())
            {
                string s = proxy.HelloIndigo();
                Console.WriteLine(s);
                Console.WriteLine("Press <Enter> to exit program.");
                Console.ReadLine();
            }
        }
    }
}
