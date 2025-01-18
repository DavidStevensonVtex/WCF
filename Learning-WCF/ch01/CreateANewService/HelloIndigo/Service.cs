using System.ServiceModel;

namespace HelloIndigo
{
    [ServiceContract(Namespace="http://www.thatindigogirl.com/samples/2006/06")]
    internal interface IHelloIndigoService
    {
        [OperationContract]
        string HelloIndigo();
    }

    public class HelloIndigoService : IHelloIndigoService
    {
        public string HelloIndigo()
        {
            return "Hello Indigo";
        }
    }
}
