namespace BigFood_Reviews.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IReviewRepository Reviews { get; }
        Task CommitAsync();
    }

}
