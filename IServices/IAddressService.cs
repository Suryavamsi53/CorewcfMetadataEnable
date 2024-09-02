using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using CoreWCFService1.Models;

namespace CoreWCFService1.IServices
{
    [ServiceContract]
    public interface IAddressService
    {
        [OperationContract]
        Task<IEnumerable<Address>> GetAddresses();

        [OperationContract]
        Task<Address> GetAddressById(string id);

        [OperationContract]
        Task AddAddress(Address address);

        [OperationContract]
        Task UpdateAddress(string id, Address address);

        [OperationContract]
        Task DeleteAddress(string id);
    }
}
