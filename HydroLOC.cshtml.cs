using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("loc")]
public class HydroLOC : HydroView
{
    public uint prefix { get; set; } = 1;

    public string prefix_rendered =>
        show_prefix ? prefix.ToString() : string.Empty;

    public bool show_prefix { set; get; } = false;
    public string code { get; set; } = string.Empty;

    public string classname { get; set; } = String.Empty;
}