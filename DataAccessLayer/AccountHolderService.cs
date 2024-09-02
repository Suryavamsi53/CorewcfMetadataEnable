
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;
using CoreWCFService1.Models;
using CoreWCFService1.IServices;

namespace CoreWCFService1.DataAccessLayer
{
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class AccountHolderService : IAccountHolderService
    {
        //private readonly string connectionString = "your_connection_string_here";
        private readonly string _connectionString;

        public AccountHolderService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<AccountHolder>> GetAccountHoldersAsync()
        {
            var accountHolders = new List<AccountHolder>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Account_holder_table", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            accountHolders.Add(new AccountHolder
                            {
                                AccHID = reader.GetInt32(0),
                                AccNUM = reader.GetString(1),
                                AccTypeID = reader.GetInt32(2),
                                Acc_holders_N = reader.GetString(3),
                                AC_Balance = reader.GetDecimal(4),
                                CD = reader.GetDateTime(5),
                                CreatedDate = reader.GetDateTime(6),
                                CreatedBy = reader.GetString(7),
                                UpdatedDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                                UpdatedBy = reader.IsDBNull(9) ? null : reader.GetString(9)
                            });
                        }
                    }
                }
            }

            return accountHolders;
        }

        public async Task<AccountHolder> GetAccountHolderAsync(int id)
        {
            AccountHolder accountHolder = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Account_holder_table WHERE AccHID = @AccHID", connection))
                {
                    command.Parameters.AddWithValue("@AccHID", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            accountHolder = new AccountHolder
                            {
                                AccHID = reader.GetInt32(0),
                                AccNUM = reader.GetString(1),
                                AccTypeID = reader.GetInt32(2),
                                Acc_holders_N = reader.GetString(3),
                                AC_Balance = reader.GetDecimal(4),
                                CD = reader.GetDateTime(5),
                                CreatedDate = reader.GetDateTime(6),
                                CreatedBy = reader.GetString(7),
                                UpdatedDate = reader.IsDBNull(8) ? (DateTime?)null : reader.GetDateTime(8),
                                UpdatedBy = reader.IsDBNull(9) ? null : reader.GetString(9)
                            };
                        }
                    }
                }
            }

            return accountHolder;
        }

        public async Task AddAccountHolderAsync(AccountHolder accountHolder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Account_holder_table (AccHID, AccNUM, AccTypeID, Acc_holders_N, AC_Balance, CD, CreatedDate, CreatedBy) VALUES (@AccHID, @AccNUM, @AccTypeID, @Acc_holders_N, @AC_Balance, @CD, @CreatedDate, @CreatedBy)", connection))
                {
                    command.Parameters.AddWithValue("@AccHID", accountHolder.AccHID);
                    command.Parameters.AddWithValue("@AccNUM", accountHolder.AccNUM);
                    command.Parameters.AddWithValue("@AccTypeID", accountHolder.AccTypeID);
                    command.Parameters.AddWithValue("@Acc_holders_N", accountHolder.Acc_holders_N);
                    command.Parameters.AddWithValue("@AC_Balance", accountHolder.AC_Balance);
                    command.Parameters.AddWithValue("@CD", accountHolder.CD);
                    command.Parameters.AddWithValue("@CreatedDate", accountHolder.CreatedDate);
                    command.Parameters.AddWithValue("@CreatedBy", accountHolder.CreatedBy);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAccountHolderAsync(AccountHolder accountHolder)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("UPDATE Account_holder_table SET AccNUM = @AccNUM, AccTypeID = @AccTypeID, Acc_holders_N = @Acc_holders_N, AC_Balance = @AC_Balance, CD = @CD, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AccHID = @AccHID", connection))
                {
                    command.Parameters.AddWithValue("@AccHID", accountHolder.AccHID);
                    command.Parameters.AddWithValue("@AccNUM", accountHolder.AccNUM);
                    command.Parameters.AddWithValue("@AccTypeID", accountHolder.AccTypeID);
                    command.Parameters.AddWithValue("@Acc_holders_N", accountHolder.Acc_holders_N);
                    command.Parameters.AddWithValue("@AC_Balance", accountHolder.AC_Balance);
                    command.Parameters.AddWithValue("@CD", accountHolder.CD);
                    command.Parameters.AddWithValue("@UpdatedDate", accountHolder.UpdatedDate.HasValue ? (object)accountHolder.UpdatedDate.Value : CoreWCF.DBNull.Value);
                    command.Parameters.AddWithValue("@UpdatedBy", accountHolder.UpdatedBy ?? (object)CoreWCF.DBNull.Value);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAccountHolderAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DELETE FROM Account_holder_table WHERE AccHID = @AccHID", connection))
                {
                    command.Parameters.AddWithValue("@AccHID", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}