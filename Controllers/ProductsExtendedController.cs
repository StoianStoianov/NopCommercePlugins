using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Blogs;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Common;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Topics;
using Nop.Services.Vendors;
using Nop.Web.Controllers;
using Nop.Web.Framework.Security.Captcha;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Controllers
{
    public class ProductsExtendedController:ProductController
    {
        private readonly ISettingService settingService;
        private readonly IProductService productService;
        public ProductsExtendedController(ISettingService settingService, ICategoryService categoryService, IManufacturerService manufacturerService, IProductService productService, IVendorService vendorService, IProductTemplateService productTemplateService, IProductAttributeService productAttributeService, IWorkContext workContext, IStoreContext storeContext, ITaxService taxService, ICurrencyService currencyService, IPictureService pictureService, ILocalizationService localizationService, IMeasureService measureService, IPriceCalculationService priceCalculationService, IPriceFormatter priceFormatter, IWebHelper webHelper, ISpecificationAttributeService specificationAttributeService, IDateTimeHelper dateTimeHelper, IRecentlyViewedProductsService recentlyViewedProductsService, ICompareProductsService compareProductsService, IWorkflowMessageService workflowMessageService, IProductTagService productTagService, IOrderReportService orderReportService, IAclService aclService, IStoreMappingService storeMappingService, IPermissionService permissionService, IDownloadService downloadService, ICustomerActivityService customerActivityService, IProductAttributeParser productAttributeParser, IShippingService shippingService, IEventPublisher eventPublisher, MediaSettings mediaSettings, CatalogSettings catalogSettings, VendorSettings vendorSettings, ShoppingCartSettings shoppingCartSettings, LocalizationSettings localizationSettings, CustomerSettings customerSettings, CaptchaSettings captchaSettings, SeoSettings seoSettings, ICacheManager cacheManager) : base(categoryService, manufacturerService, productService, vendorService, productTemplateService, productAttributeService, workContext, storeContext, taxService, currencyService, pictureService, localizationService, measureService, priceCalculationService, priceFormatter, webHelper, specificationAttributeService, dateTimeHelper, recentlyViewedProductsService, compareProductsService, workflowMessageService, productTagService, orderReportService, aclService, storeMappingService, permissionService, downloadService, customerActivityService, productAttributeParser, shippingService, eventPublisher, mediaSettings, catalogSettings, vendorSettings, shoppingCartSettings, localizationSettings, customerSettings, captchaSettings, seoSettings, cacheManager)
        {
            this.settingService = settingService;
            this.productService = productService;
        }

        public ActionResult GetNewestProducts(int? productThumbPictureSize)
        {
            var productToTakeNumber = int.Parse(this.settingService.GetSettingByKey<string>(Constants.ProductToShowSettingName));
            var products = productService.SearchProducts(markedAsNewOnly: true).Take(productToTakeNumber).ToList();
         
            if (products.Count == 0)
                return Content("");

            //prepare model
            var model = this.PrepareProductOverviewModels(products, true, true, productThumbPictureSize).ToList();            
            return View("~/Plugins/Widgets.HomePageNewProductsPlugin/Views/ProductsExtended/GetNewestProducts.cshtml",model);
        }
    }
}