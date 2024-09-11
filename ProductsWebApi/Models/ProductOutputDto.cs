namespace ProductsWebApi.Models
{
  public class ProductOutputDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    /// <summary>
    /// Euro
    /// </summary>
    public decimal Price { get; set; }

    public StoreOutputDto Store { get; set; }
  }
}
