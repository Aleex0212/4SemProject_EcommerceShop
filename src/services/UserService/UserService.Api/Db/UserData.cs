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
        Id = Guid.NewGuid(),
        Name = "User1",
        Email = "User1Email@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.Customer,
        Phone = "11111111",
        Address = "User1Address"
      };
      var user2 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "User2",
        Email = "User2Email@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.Customer,
        Phone = "22222222",
        Address = "User2Address"
      };
      var user3 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "User3",
        Email = "User3Email@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.Admin,
        Phone = "33333333",
        Address = "User3Address"
      };
      var user4 = new UserDto()
      {
        Id = Guid.NewGuid(),
        Name = "User4",
        Email = "User4Email@Mail.dk",
        Password = "3HMfKV5eMAsi7f/UlFnWVgd34dJYEsVZbUV4Em1qe1U=", //UserPassword1! + salt : "super-secret-salt-value_xd"
        UserType = UserTypes.User,
        Phone = "44444444",
        Address = "User4Address"
      };
      Users.Add(user1);
      Users.Add(user2);
      Users.Add(user3);
      Users.Add(user4);
    }
  }
}
