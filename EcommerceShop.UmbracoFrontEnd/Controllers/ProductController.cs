using EcommerceShop.Common.Dto;
using EcommerceShop.UmbracoFrontEnd;
using Polly.CircuitBreaker;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace EcommerceShop.UmbracoFrontEnd.Controllers
{
  public class ProductController : SurfaceController

  {
    public ProductController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
      ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider)
      : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
    }

    public void SetProducts(IEnumerable<ProductDto> products, IEnumerable<IPublishedContent> existingProducts)
    {
      if (!products.Any()) return;


      var cs = Services.ContentService;
      if (cs != null)
      {
        foreach (var product in products)
        {
          if (!existingProducts.Any(x => x.Name == product.Name))
            {
            IContent umbracoProduct = cs.Create(product.Name, CurrentPage.Id, "product");
            umbracoProduct.SetValue("identifier", product.Id);
            umbracoProduct.SetValue("price", product.Price);
            umbracoProduct.SetValue("amountLeft", product.Amount);
            cs.Save(umbracoProduct);
          }
        }
      }
      
    }
  }
}
