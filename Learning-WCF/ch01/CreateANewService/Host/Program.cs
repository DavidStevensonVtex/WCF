using System;

namespace Host
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MyServiceHost.StartService();
                Console.WriteLine("Press <ENTER> to terminate the service host");
                Console.ReadLine();
            }
            finally
            {
                MyServiceHost.StopService();
            }
        }
    }
}
