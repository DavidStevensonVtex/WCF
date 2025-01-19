using System.ServiceModel;

namespace Client
{
    internal class ServiceProxy
    {
        [ServiceContract(Namespace = "http://www.thatindigogirl.com/samples/2006/06")]
        public interface IHelloIndigoService
        {
            [OperationContract]
            string HelloIndigo();
        }
    }
}
