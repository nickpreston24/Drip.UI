using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.Htmx;

[HtmlTargetElement("hxloader")]
public class HxIndicator : HydroView
{
    public string id { get; set; } = string.Empty;
    public string message { get; set; } = string.Empty;
}