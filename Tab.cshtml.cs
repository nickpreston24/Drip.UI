using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace sharpify_web.Pages.Sandbox.Components;

[HtmlTargetElement("hydro-tab")]
public class Tab : HydroView
{
    public string tabname { get; set; } = string.Empty;
}