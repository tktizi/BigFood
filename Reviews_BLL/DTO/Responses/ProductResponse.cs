namespace Catalog_BLL.DTO.Responses
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ShortCategoryResponse Category { get; set; } 
    }


}
