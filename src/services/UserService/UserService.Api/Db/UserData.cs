using EcommerceShop.Common.Dto;
using EcommerceShop.Common.Enum;

namespace UserService.Api.Db
{
  public class UserData
  {
    public List<UserDto> Users { get; }

    public UserData()
    {
      Users = new List<UserDto>();

      var user1 = new UserDto()
      {
        Id = new Guid("11111111-1111-1111-1111-111111111111"),
        Name = "User1",
        Email = "Customer@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.Customer,
        Phone = "22222222",
        Address = "User2Address"
      };
      var user2 = new UserDto()
      {
        Id = new Guid("22222222-2222-2222-2222-222222222222"),
        Name = "User2",
        Email = "Admin@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.Admin,
        Phone = "33333333",
        Address = "User3Address"
      };
      var user3 = new UserDto()
      {
        Id = new Guid("33333333-3333-3333-3333-333333333333"),
        Name = "User3",
        Email = "User@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.User,
        Phone = "44444444",
        Address = "User4Address"
      };
      Users.Add(user1);
      Users.Add(user2);
      Users.Add(user3);
    }
  }
}
