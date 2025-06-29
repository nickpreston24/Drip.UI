using Hydro;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("dashboard")]
public class DaisyDashboard : HydroView
{
    public string Name { get; set; } = Guid.NewGuid().ToString() + "_dashboard";
}