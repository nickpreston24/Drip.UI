using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("modal")]
public class HydroModal : HydroView
{
    public string Title { get; set; } = String.Empty;
    public string classname { get; set; } = string.Empty;
}