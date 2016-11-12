using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
