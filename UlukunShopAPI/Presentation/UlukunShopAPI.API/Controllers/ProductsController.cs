using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UlukunShopAPI.Application.Repositories;
using UlukunShopAPI.Domain.Entities;


namespace UlukunShopAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRespository _productReadRespository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        readonly private IOrderReadRepository _orderReadRepository;

        public ProductsController(IProductReadRespository productReadRespository,
            IProductWriteRepository productWriteRepository, IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _productReadRespository = productReadRespository;
            _productWriteRepository = productWriteRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("sadece belirli adresten gelen isteklere cevap okkk");
        }
    }
}