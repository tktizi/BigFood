namespace Catalog_BLL.DTO.Requests
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }  
    }

}
