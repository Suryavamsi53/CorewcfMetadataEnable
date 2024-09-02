using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CoreWCFService1.Models;
using CoreWCFService1.IServices;

namespace CoreWCFService1.DataAccessLayer
{
    public class AccountService : IAccountService
    {
        private readonly string _connectionString;

        public AccountService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            Account account = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Account_table WHERE AccId = @AccId", connection);
                command.Parameters.AddWithValue("@AccId", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        account = new Account
                        {
                            AccId = reader.GetInt32(reader.GetOrdinal("AccId")),
                            AccountNumber = reader.GetString(reader.GetOrdinal("AccountNumber")),
                            AccountStatus_id = reader.GetInt32(reader.GetOrdinal("AccountStatus_id")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : reader.GetString(reader.GetOrdinal("CreatedBy")),
                            UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                            UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                        };
                    }
                }
            }

            return account;
        }

        public async Task AddAccountAsync(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Account_table (AccId, AccountNumber, AccountStatus_id, CreatedDate, CreatedBy) " +
                    "VALUES (@AccId, @AccountNumber, @AccountStatus_id, @CreatedDate, @CreatedBy)", connection);
                command.Parameters.AddWithValue("@AccId", account.AccId);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
                command.Parameters.AddWithValue("@CreatedDate", account.CreatedDate);
                command.Parameters.AddWithValue("@CreatedBy", account.CreatedBy ?? (object)System.DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Account_table SET AccountNumber = @AccountNumber, AccountStatus_id = @AccountStatus_id, " +
                    "UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AccId = @AccId", connection);
                command.Parameters.AddWithValue("@AccId", account.AccId);
                command.Parameters.AddWithValue("@AccountNumber", account.AccountNumber ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@AccountStatus_id", account.AccountStatus_id);
                command.Parameters.AddWithValue("@UpdatedDate", account.UpdatedDate ?? (object)System.DBNull.Value);
                command.Parameters.AddWithValue("@UpdatedBy", account.UpdatedBy ?? (object)System.DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAccountAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Account_table WHERE AccId = @AccId", connection);
                command.Parameters.AddWithValue("@AccId", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
