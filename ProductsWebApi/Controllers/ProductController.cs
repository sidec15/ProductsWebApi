using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsWebApi.Dal;
using ProductsWebApi.Models;

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
      var product = _mapper.Map<Product>(productDto);

      _dbContext.Products.Add(product);
      await _dbContext.SaveChangesAsync();

      return Created();
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

  }
}
