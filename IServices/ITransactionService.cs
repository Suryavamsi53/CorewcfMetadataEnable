using CoreWCFService1.Models;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CoreWCFService1.IServices
{
    [CoreWCF.ServiceContract]
    public interface ITransactionService
    {
        [OperationContract]
        Task<List<Transaction>> GetTransactionsAsync();

        [CoreWCF.OperationContract]
        Task<Transaction> GetTransactionAsync(int id);

        [CoreWCF.OperationContract]
        Task AddTransactionAsync(Transaction transaction);

        [CoreWCF.OperationContract]
        Task UpdateTransactionAsync(Transaction transaction);

        [CoreWCF.OperationContract]
        Task DeleteTransactionAsync(int id);
    }
}
