using UserService.Db;
using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Routes;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Api.Controllers
{
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly CustomerData _customerData;
    public UserController(CustomerData customerData)
    {
      _customerData = customerData;
    }

    [HttpPost(Routes.CustomerRoutes.BaseUrl)]
    public IActionResult VerifyCustomer([FromBody] CustomerDto customer)
    {
      _customerData.Customers.First(c => c.Id == customer.Id);
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
