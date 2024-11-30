using EcommerceShop.Common.Dto;
using EcommerceShop.Umraco.Refit;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Models;
using Umbraco.Cms.Web.Website.Controllers;

namespace EcommerceShop.Umraco.Controllers
{
  public class LoginController : SurfaceController
  {
    private readonly IAuthApi _authApi;
    private readonly IUserApi _userApi;
    private readonly SessionStore _sessionStore;
    private readonly LoggedInUser _loggedInUser;

    public LoginController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
      ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider,
      IAuthApi authApi, SessionStore sessionStore, LoggedInUser loggedInUser, IUserApi userApi)
      : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
      _authApi = authApi;
      _sessionStore = sessionStore;
      _loggedInUser = loggedInUser;
      _userApi = userApi;
    }
    [HttpPost]
    public async Task<IActionResult> LoginToSession(LoginModel loginModel)
    {
      if (!ModelState.IsValid)
      {
        // Return View with exception
        return CurrentUmbracoPage();
      }

      var loginDto = new LoginDto
      {
        Email = loginModel.Username,
        HashedPassword = HashPassword(loginModel.Password)
      };

      try
      {
        var token = await _authApi.Login(loginDto);

        if (!string.IsNullOrEmpty(token))
        {
          _sessionStore.SetSessionToken(token);
        _loggedInUser.User = await _userApi.GetCustomerByEmail(loginDto.Email);
          return Redirect(loginModel.RedirectUrl);
        }
        ModelState.AddModelError("", "Login failed. Please try again.");
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Login failed. Please try again.");
      }
      //Return to current page
      return CurrentUmbracoPage();
    }

    [HttpPost]
    public IActionResult LogOut(PostRedirectModel redirectModel)
    {
      try
      {
        _sessionStore.ClearSessionToken();
        _loggedInUser.User = null;
        return Redirect(redirectModel.RedirectUrl);
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Log out failed. Please try again.");
        return CurrentUmbracoPage();
      }
    }

    private string HashPassword(string password)
    {
      string passwordToHash = password; //UserPassword1!
      string saltValue = "super-secret-salt-value_xd";
      byte[] salt = Encoding.UTF8.GetBytes(saltValue!);

      string hashedPasswordSalt = Convert.ToBase64String
      (KeyDerivation.Pbkdf2
        (
          password: passwordToHash,
          salt: salt,
          prf: KeyDerivationPrf.HMACSHA256,
          iterationCount: 10,
          numBytesRequested: 256 / 8)
      );
      return hashedPasswordSalt;
    }
  }
}
