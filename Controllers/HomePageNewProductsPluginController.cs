using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Common;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Controllers
{
    public class HomePageNewProductsPluginController : BasePluginController
    {
        private readonly ISettingService _settingService;
        private readonly IProductService _productService;
        public HomePageNewProductsPluginController(ISettingService settingService, IProductService productService)
        {
            this._settingService = settingService;
            this._productService = productService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        [HttpGet]
        public ActionResult Configure()
        {
            var widgetZoneSettingValue = this._settingService.GetSettingByKey<string>(Constants.WidgetZoneSettingName);
            var productsToShowSettingValue = this._settingService.GetSettingByKey<string>(Constants.ProductToShowSettingName);
            var model = new HomePageNewProductsConfigureModel()
            {
                WidgetZone = widgetZoneSettingValue,
                ProductsToShow = int.Parse(productsToShowSettingValue)
            };

            return View("~/Plugins/Widgets.HomePageNewProductsPlugin/Views/HomePageNewProductsPlugin/Configure.cshtml",model);
        }

        [AdminAuthorize]
        [ChildActionOnly]
        [HttpPost]
        public ActionResult Configure(HomePageNewProductsConfigureModel homePageNewProductsConfigureModel)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Plugins/Widgets.HomePageNewProductsPlugin/Views/HomePageNewProductsPlugin/Configure.cshtml");
            }

            this._settingService.SetSetting(Constants.WidgetZoneSettingName,homePageNewProductsConfigureModel.WidgetZone);
            this._settingService.SetSetting(Constants.ProductToShowSettingName,homePageNewProductsConfigureModel.ProductsToShow);

            return View("~/Plugins/Widgets.HomePageNewProductsPlugin/Views/HomePageNewProductsPlugin/Configure.cshtml");

        }
        [ChildActionOnly]
        public ActionResult GetNewestProducts()
        {


            var productToTakeNumber = int.Parse(this._settingService.GetSettingByKey<string>(Constants.ProductToShowSettingName));
            var products = _productService.SearchProducts(markedAsNewOnly: true).Take(productToTakeNumber);

            //var model = new List<ProductOverviewModel>();
            return View("~/Plugins/Widgets.HomePageNewProductsPlugin/Views/HomePageNewProductsPlugin/GetNewestProducts.cshtml");
        }

    }
}
