using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("hydro-nav")]
public class HydroNav : HydroView
{
    public string Title { get; set; } = nameof(sharpify_web);
    public List<NavbarLink> links { get; set; } = new();
}

public record NavbarLink(string text = "Home", string url = "/", string icon = "")
{
    public List<NavbarLink> children { get; set; } = new();
}