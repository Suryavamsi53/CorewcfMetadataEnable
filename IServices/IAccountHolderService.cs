using CoreWCFService1.Models;

namespace CoreWCFService1.IServices
{ 
      [ServiceContract]
        public interface IAccountHolderService
        {
            [OperationContract]
            Task<List<AccountHolder>> GetAccountHoldersAsync();

            [OperationContract]
            Task<AccountHolder> GetAccountHolderAsync(int id);

            [OperationContract]
            Task AddAccountHolderAsync(AccountHolder accountHolder);

            [OperationContract]
            Task UpdateAccountHolderAsync(AccountHolder accountHolder);

            [OperationContract]
            Task DeleteAccountHolderAsync(int id);
        }
    }
