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
  public class StoreController : ControllerBase
  {

    private readonly ILogger<StoreController> _logger;
    private readonly ProductDbContext _dbContext;
    private readonly IMapper _mapper;

    public StoreController(ILogger<StoreController> logger, ProductDbContext dbContext, IMapper mapper)
    {
      _logger = logger;
      _dbContext = dbContext;
      _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] StoreInputDto dto)
    {
      var store = _mapper.Map<Store>(dto);

      _dbContext.Stores.Add(store);
      await _dbContext.SaveChangesAsync();

      return Created();
    }

    [HttpGet]
    public async Task<ActionResult<StoreOutputDto[]>> GetAllAsync()
    {

      var stores = await _dbContext
        .Stores
        .ProjectTo<StoreOutputDto>(_mapper.ConfigurationProvider)
        .ToListAsync();

      return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoreOutputDto[]>> GetByIdAsync([FromRoute] Guid id)
    {

      var store = await _dbContext
        .Stores
        .Where(x => x.Id == id)
        .ProjectTo<StoreOutputDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

      if(store == null)
        return NotFound();

      return Ok(store);
    }


  }
}
