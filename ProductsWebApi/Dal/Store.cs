namespace ProductsWebApi.Dal
{
  public class Store
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];

  }

}
