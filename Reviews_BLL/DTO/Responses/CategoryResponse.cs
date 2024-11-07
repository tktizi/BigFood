namespace Catalog_BLL.DTO.Responses
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ShortProductResponse> Products { get; set; }  
    }

}
