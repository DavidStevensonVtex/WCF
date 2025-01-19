using System.ServiceModel;

namespace Host
{
    internal class MyServiceHost
    {
        static ServiceHost myServiceHost = null;

        internal static void StartService()
        {
            myServiceHost = new ServiceHost(typeof(HelloIndigoService));
            myServiceHost.Open();
        }

        internal static void StopService()
        {
            myServiceHost.Close();
        }
    }
}
