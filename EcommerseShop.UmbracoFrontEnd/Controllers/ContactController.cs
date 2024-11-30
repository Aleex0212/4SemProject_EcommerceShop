
using EcommerceShop.UmbracoFrontEnd.ViewModels;
using EcommerseShop.UmbracoFrontEnd.SessionStores;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace EcommerceShop.UmbracoFrontEnd.Controllers
{
  public class ContactController : SurfaceController
  {
    private readonly LoggedInUser _loggedInUser;
    public ContactController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
      ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger,
      IPublishedUrlProvider publishedUrlProvider, LoggedInUser loggedInUser) :
      base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
      _loggedInUser = loggedInUser;
    }

    [HttpPost]
    public IActionResult Submit(ContactForm contact)
    {
      if (!ModelState.IsValid)
      {
        return CurrentUmbracoPage();
      }


      TempData["success"] = true;

      var cs = Services.ContentService;
      if (cs != null)
      {
        IContent comment = cs.Create(_loggedInUser.User.UserType.ToString(), CurrentPage.Id, "Comment");
        comment.SetValue("username", _loggedInUser.User.Name);
        comment.SetValue("email", _loggedInUser.User.Email);
        comment.SetValue("message", contact.Message);
        cs.Save(comment);
      }
      //Succes - go back to current page
      return RedirectToCurrentUmbracoPage();
    }
  }
}
