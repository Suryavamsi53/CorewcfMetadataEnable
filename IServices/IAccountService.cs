using CoreWCFService1.Models;
using System.ServiceModel;
using CoreWCFService1.DataAccessLayer;
namespace CoreWCFService1.IServices
{
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        Task<Account> GetAccountByIdAsync(int id);

        [OperationContract]
        Task AddAccountAsync(Account account);

        [OperationContract]
        Task UpdateAccountAsync(Account account);

        [OperationContract]
        Task DeleteAccountAsync(int id);
    }
}
