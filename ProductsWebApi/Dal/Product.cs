
namespace ProductsWebApi.Dal
{
  public class Product
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public Guid StoreId { get; set; }
    public virtual Store Store { get; set; }

  }


}
