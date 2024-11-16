using CustomerService.Db;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly CustomerData _customerData;
    public CustomerController(CustomerData customerData)
    {
      _customerData = customerData;
    }

    [HttpPost(Routes.CustomerRoutes.BaseUrl)]
    public IActionResult VerifyCustomer([FromBody] CustomerDto customer)
    {
      return Ok();
    }

    [HttpPost(Routes.CustomerRoutes.Login)]
    public ActionResult<CustomerDto> Login([FromBody] LoginDto loginDto)
    {
      var customer = _customerData.Customers.First(c => c.Email == loginDto.Email && c.Password == loginDto.Password);
      return customer;
    }
  }
}
