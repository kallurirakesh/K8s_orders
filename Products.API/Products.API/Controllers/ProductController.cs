using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ProductsService _service;

        public ProductController(ILogger<ProductController> logger, ProductsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _service.GetAsync();
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var p = await _service.GetAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] Product product)
        {
            await _service.CreateAsync(product);
            return CreatedAtRoute("GetProductById", new { id = product.Id }, product);
        }
    }
}
