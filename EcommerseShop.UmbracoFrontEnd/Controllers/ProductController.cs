using EcommerceShop.Common.Dto;
using EcommerseShop.UmbracoFrontEnd.SessionStores;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace EcommerseShop.UmbracoFrontEnd.Controllers
{
  public class ProductController : SurfaceController
  {
    private readonly Basket cart;
    public ProductController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory,
      ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, Basket cart)
      : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
      this.cart = cart;
    }

    /// <summary>
    /// Cannot get working
    /// </summary>
    /// <param name="products"></param>
    /// <param name="existingProducts"></param>
    [HttpPost]
    public void SetProducts(IEnumerable<ProductDto> products, IEnumerable<string> existingProducts)
    {

      IContentService cs = Services.ContentService;
      if (cs is null) return;
      foreach (var product in products)
      {
        if (!existingProducts.Any(x => x == product.Name))
        {
          IContent umbracoProduct = cs.Create(product.Name, CurrentPage.Id, "product");
          umbracoProduct.SetValue("identifier", product.Id);
          umbracoProduct.SetValue("price", product.Price);
          umbracoProduct.SetValue("amountLeft", product.Amount);
          cs.Save(umbracoProduct);
        }
      }
      return;
    }

    [HttpPost]
    public IActionResult AddToCart(ProductDto product)
    {
      if (cart.Products.Any(x => x.Product.Id == product.Id))
      {
        cart.Products.First(x => x.Product.Id == product.Id).Quantity++;
      }
      else
      {
        var productLine = new ProductLineDto();
        productLine.Id = Guid.NewGuid();
        productLine.Product = product;
        productLine.Quantity = 1;
        cart.Products.Add(productLine);
      }
      return CurrentUmbracoPage();

    }
  }
}
