using BigFood_Reviews.Entities;
using BigFood_Reviews.Repositories.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Data;

namespace BigFood_Reviews.Repositories
{

    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly NpgsqlConnection _dbConnection;

        public ReviewRepository(NpgsqlConnection conn, IDbTransaction dbTransaction) : base(conn, dbTransaction, "Reviews")
        {
            _dbConnection = conn;
        }


        public async Task AddReviewAsync(Review review)
        {
            var query = "INSERT INTO Reviews (ReviewId, CustomerId, Rating, Comment) VALUES (@ReviewId, @CustomerId, @Rating, @Comment)";
            {
                await _dbConnection.ExecuteAsync(query, review);
            }
        }




        public async Task<IEnumerable<Review>> GetReviewsWithCustomersAsync()
        {
            var sql = @"
            SELECT 
                r.Id, 
                r.Rating, 
                r.Comment, 
                r.CustomerId,
                r.ProductId,
                c.Id AS CustomerId, 
                c.Id,
                c.Name,   
                c.Email,
                c.PhoneNumber
            FROM 
                Reviews r 
            INNER JOIN 
                Customers c ON r.CustomerId = c.Id;";

            var reviews = await _dbConnection.QueryAsync<Review, Customer, Review>(
                sql,
                (review, customer) =>
                {
                    review.Customer = customer; 
                    return review;
                },
                splitOn: "CustomerId" 
            );

            return reviews;
        }



        public async Task<IEnumerable<Review>> GetAllReviewsByProductIdAsync(int productId) 
        { 
            var query = "SELECT * FROM Reviews r WHERE r.ProductId = @ProductId;";
            var sql = @"
            SELECT 
                r.Id, 
                r.Rating, 
                r.Comment, 
                r.CustomerId, 
                r.ProductId,
                c.Id AS CustomerId, 
                c.Id,
                c.Name,   
                c.Email,
                c.PhoneNumber
            FROM 
                Reviews r 
            INNER JOIN 
                Customers c ON r.CustomerId = c.Id WHERE r.ProductId = @ProductId;";

            var reviews = await _dbConnection.QueryAsync<Review, Customer, Review>(
                sql,
                (review, customer) =>
                {
                    review.Customer = customer;
                    return review;
                },
                param: new { ProductId = productId },
                splitOn: "CustomerId"
            );

            return reviews;
            //var reviews = await _dbConnection.QueryAsync<Review>(query, new { ProductId = productId }); 
            //return reviews; 
        }
    }
}
