using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Widgets.HomePageNewProductsPlugin.Models
{
    public class HomePageNewProductsConfigureModel
    {
        [Required]
        public string WidgetZone { get; set; }
        [Required]
        [Range(1, 40)]
        public int ProductsToShow { get; set; }
    }
}
