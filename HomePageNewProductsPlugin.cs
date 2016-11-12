using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using System.Collections.Generic;
using System.Web.Routing;
using Nop.Plugin.Widgets.HomePageNewProductsPlugin.Common;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin
{
    public class HomePageNewProductsPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;

        public HomePageNewProductsPlugin(ISettingService settingService)
        {
            this._settingService = settingService;
        }
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "HomePageNewProductsPlugin";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Widgets.HomePageNewProductsPlugin.Controllers" }, { "area", null } };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "GetNewestProducts";
            controllerName = "ProductsExtended";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.HomePageNewProductsPlugin.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        public IList<string> GetWidgetZones()
        {
            var widgetZone = this._settingService.GetSettingByKey<string>(Constants.WidgetZoneSettingName);

            return new List<string>
            {
               widgetZone
            };
        }

        public override void Install()
        {
            this._settingService.SetSetting(Constants.WidgetZoneSettingName, Constants.WidgetZoneSettingInitialValue);
            this._settingService.SetSetting(Constants.ProductToShowSettingName, Constants.ProductToShowInitialValue);

            base.Install();
        }

        public override void Uninstall()
        {
            var widgetZoneSetting = this._settingService.GetSetting(Constants.WidgetZoneSettingName);
            var productsToShowSetting = this._settingService.GetSetting(Constants.ProductToShowSettingName);       
            this._settingService.DeleteSetting(widgetZoneSetting);
            this._settingService.DeleteSetting(productsToShowSetting);
            base.Uninstall();
        }
    }
}
