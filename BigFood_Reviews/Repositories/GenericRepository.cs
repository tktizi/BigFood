using BigFood_Reviews.Repositories.Interfaces;
using Dapper;
using Npgsql;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace BigFood_Reviews.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected NpgsqlConnection _sqlConnection;

        protected IDbTransaction _dbTransaction;

        protected readonly string _tableName;

        protected GenericRepository(NpgsqlConnection sqlConnection,
            IDbTransaction dbTransaction,
            string tableName)
        {
            _sqlConnection = sqlConnection;
            _dbTransaction = dbTransaction;
            _tableName = tableName;
        }


        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _sqlConnection.QueryAsync<T>($"SELECT * FROM {_tableName}",
                transaction: _dbTransaction);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            var result = await _sqlConnection.QuerySingleOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE id=@id",
                param: new { id = id },
                transaction: _dbTransaction);
            if (result == null)
                throw new KeyNotFoundException($"{_tableName} with id [{id}] could not be found.");
            return result;
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _sqlConnection.ExecuteAsync($"DELETE FROM {_tableName} WHERE id=@id",
                param: new { id = id },
                transaction: _dbTransaction);
        }

        public virtual async Task<int> AddAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();
            //var query = "INSERT INTO categories (category_name,description) VALUES ('something','test');";

            var newId = await _sqlConnection.ExecuteScalarAsync<int>(insertQuery,
                param: t,
                transaction: _dbTransaction);
            return newId;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            inserted += await _sqlConnection.ExecuteAsync(query,
                param: list);
            return inserted;
        }


        public virtual async Task ReplaceAsync(T t, int? id = null)
        {
            var updateQuery = GenerateUpdateQuery();
            await _sqlConnection.ExecuteAsync(updateQuery,
                param: t,
                transaction: _dbTransaction);
        }

        // работа со свойствами модели
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            var result = (from prop in listOfProperties
                          let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                          where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                          select prop.Name).ToList();
            result.Remove("Id");
            result.Remove("Customer");
            return result;
        }

        // генерация Update выражения
        private string GenerateUpdateQuery(int? id = null)
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }

            });
            updateQuery.Remove(updateQuery.Length - 1, 1); //убираем последнию запятую
            var _ = id.HasValue
                ? updateQuery.Append($" WHERE id={id.Value};")
                : updateQuery.Append(" WHERE id=@id;");
            return updateQuery.ToString();
        }

        // генерация Isert выражения
        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);
            //при условии что РК - автоинкремент
            //properties.Remove("id");
            properties.Remove("updated_at");
            //
            properties.ForEach(prop => { insertQuery.Append($"{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(");");
            //insertQuery.Append("; SELECT SCOPE_IDENTITY()");
            Console.WriteLine(insertQuery.ToString());
            return insertQuery.ToString();
        }
    }
}
