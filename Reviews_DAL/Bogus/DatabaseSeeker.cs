using Bogus;
using Catalog_DAL.Entities;

namespace Orders.DAL.Bogus 
{  

    public class DatabaseSeeder
    {
        public IReadOnlyCollection<Product> Products { get; set; } = new List<Product>();
        public IReadOnlyCollection<Category> Categories { get; set; } = new List<Category>();

        public DatabaseSeeder()
        {
            Categories = GenerateCategories(amount: 10);
            Products = GenerateProducts(amount: 10, Categories);
        }

        private IReadOnlyCollection<Category> GenerateCategories(int amount)
        {
            var category = new[] { "fruits", "vegetables" };
            int Id = 1;
            var categoriesFake = new Faker<Category>()
                .RuleFor(p => p.Id, f => Id++)
                .RuleFor(p => p.Name, f => f.PickRandom(category));

            var categories = categoriesFake.Generate(amount);
            return categories;
        }

        private IReadOnlyCollection<Product> GenerateProducts(int amount, IEnumerable<Category> categories)
        {
            var fruit = new[] { "apple", "banana", "orange", "strawberry", "peach" };
            int Id = 1;
            var productsFaker = new Faker<Product>()
                .RuleFor(o => o.Id, f => Id++)
                .RuleFor(o => o.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(o => o.Name, f => f.PickRandom(fruit))
                .RuleFor(o => o.Price, f => f.Random.Number(1, 100));
            var products = productsFaker.Generate(amount);
            return products;
        }
    }
}