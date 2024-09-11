namespace ProductsWebApi.Models
{
  public class ProductInputDto
  {
    public string Name { get; set; }
    /// <summary>
    /// Euro
    /// </summary>
    public decimal Price { get; set; }
    public Guid StoreId { get; set; }

  }
}
