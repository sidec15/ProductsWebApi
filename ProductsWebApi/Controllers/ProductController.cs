using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebApi.Dal;
using ProductsWebApi.Models;
using ProductsWebApi.Services;

namespace ProductsWebApi.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductController : ControllerBase
  {

    private readonly ILogger<ProductController> _logger;
    private readonly ProductDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductController(ILogger<ProductController> logger, ProductDbContext dbContext, IMapper mapper)
    {
      _logger = logger;
      _dbContext = dbContext;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] ProductInputDto productDto)
    {
      var entity = _mapper.Map<Product>(productDto);

      _dbContext.Products.Add(entity);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction(
          nameof(GetByIdAsync),
          new { id = entity.Id.ToString() },  // Ensure you pass the correct route values
          entity.Id                // Optionally, you can return the entity or some relevant data
          );
    }

    [HttpGet]
    public async Task<ActionResult<ProductOutputDto>> GetAllAsync()
    {

      var products = await _dbContext
        .Products
        .ProjectTo<ProductOutputDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

      return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductOutputDto[]>> GetByIdAsync([FromRoute] Guid id)
    {

      var product = await _dbContext
        .Products
        .Where(x => x.Id == id)
        .ProjectTo<ProductOutputDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

      if (product == null)
        return NotFound();

      return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
    {

      var product = await _dbContext
        .Products
        .Where(x => x.Id == id)
        .FirstOrDefaultAsync();

      if (product != null)
      {
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
      }


      return Ok(product);
    }

    /// <summary>
    /// Move the product to a different store
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}/move")]
    public async Task<IActionResult> MoveAsync([FromRoute] Guid id,[FromBody] ProductModeInputDto dto)
    {
      var product = await _dbContext.Products.FindAsync(id);

      product.StoreId = dto.StoreId;
      
      await _dbContext.SaveChangesAsync();

      return Ok();
    }

  }
}
