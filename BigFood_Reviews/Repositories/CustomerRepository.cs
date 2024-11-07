using BigFood_Reviews.Entities;
using BigFood_Reviews.Repositories.Interfaces;
using Dapper;
using Npgsql;
using System.Data;

namespace BigFood_Reviews.Repositories
{
    public class CustomerRepository : GenericRepository <Customer>, ICustomerRepository
    {
        private readonly NpgsqlConnection _dbConnection;

        public CustomerRepository(NpgsqlConnection conn, IDbTransaction dbTransaction) : base(conn, dbTransaction, "Customers")
        {
            _dbConnection = conn;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {

                var query = "SELECT * FROM Customers";
                return await _dbConnection.QueryAsync<Customer>(query);
            
        }


        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var query = "SELECT * FROM Customers WHERE Email = @Email";
            {
                await _dbConnection.OpenAsync();
                using (var command = new NpgsqlCommand(query, _dbConnection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Customer
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                PhoneNumber = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

    }

}
