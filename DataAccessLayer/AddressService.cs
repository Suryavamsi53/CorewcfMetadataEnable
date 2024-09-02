using CoreWCFService1.IServices;
using CoreWCFService1.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Threading.Tasks;

namespace CoreWCFService1.DataAccessLayer

{ 
public class AddressService : IAddressService
{
    private readonly string _connectionString;

    public AddressService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Address>> GetAddresses()
    {
        var addresses = new List<Address>();

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Address_table", connection);
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    addresses.Add(new Address
                    {
                        AddressID = reader.GetInt32(reader.GetOrdinal("AddressID")),
                        AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                        AddressTypeID = reader.GetInt32(reader.GetOrdinal("AddressTypeID")),
                        Addres = reader.GetString(reader.GetOrdinal("Address")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    });
                }
            }
        }

        return addresses;
    }

    public async Task<Address> GetAddressById(string id)
    {
        Address address = null;

        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("SELECT * FROM Address_table WHERE AddressID = @AddressID", connection);
            command.Parameters.AddWithValue("@AddressID", int.Parse(id));
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    address = new Address
                    {
                        AddressID = reader.GetInt32(reader.GetOrdinal("AddressID")),
                        AccountID = reader.GetInt32(reader.GetOrdinal("AccountID")),
                        AddressTypeID = reader.GetInt32(reader.GetOrdinal("AddressTypeID")),
                        Addres = reader.GetString(reader.GetOrdinal("Address")),
                        CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                        CreatedBy = reader.GetString(reader.GetOrdinal("CreatedBy")),
                        UpdatedDate = reader.IsDBNull(reader.GetOrdinal("UpdatedDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                        UpdatedBy = reader.IsDBNull(reader.GetOrdinal("UpdatedBy")) ? null : reader.GetString(reader.GetOrdinal("UpdatedBy"))
                    };
                }
            }
        }

        return address;
    }

    public async Task AddAddress(Address address)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "INSERT INTO Address_table (AddressID, AccountID, AddressTypeID, Address, CreatedDate, CreatedBy) " +
                "VALUES (@AddressID, @AccountID, @AddressTypeID, @Address, @CreatedDate, @CreatedBy)", connection);
            command.Parameters.AddWithValue("@AddressID", address.AddressID);
            command.Parameters.AddWithValue("@AccountID", address.AccountID);
            command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
            command.Parameters.AddWithValue("@Address", address.Addres);
            command.Parameters.AddWithValue("@CreatedDate", address.CreatedDate);
            command.Parameters.AddWithValue("@CreatedBy", address.CreatedBy);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task UpdateAddress(string id, Address address)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand(
                "UPDATE Address_table SET AccountID = @AccountID, AddressTypeID = @AddressTypeID, Address = @Address, " +
                "UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE AddressID = @AddressID", connection);
            command.Parameters.AddWithValue("@AddressID", int.Parse(id));
            command.Parameters.AddWithValue("@AccountID", address.AccountID);
            command.Parameters.AddWithValue("@AddressTypeID", address.AddressTypeID);
            command.Parameters.AddWithValue("@Address", address.Addres);
            command.Parameters.AddWithValue("@UpdatedDate", address.UpdatedDate ?? (object)System.DBNull.Value);
            command.Parameters.AddWithValue("@UpdatedBy", address.UpdatedBy ?? (object)System.DBNull.Value);

            await command.ExecuteNonQueryAsync();
        }
    }

    public async Task DeleteAddress(string id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var command = new SqlCommand("DELETE FROM Address_table WHERE AddressID = @AddressID", connection);
            command.Parameters.AddWithValue("@AddressID", int.Parse(id));

            await command.ExecuteNonQueryAsync();
        }
    }
}}