// using Microsoft.AspNetCore.Razor.TagHelpers;
//
// namespace Drip.UI.TagHelpers.Drip.UI.TagHelpers;
//
// [HtmlTargetElement("drip-island")]
// public class DripIslandTagHelper : TagHelper
// {
//     public string Page { get; set; } = "";
//     public string? Handler { get; set; }
//     public string Method { get; set; } = "get";
//     public string Trigger { get; set; } = "click";
//     public string Swap { get; set; } = "outerHTML";
//     public string? Target { get; set; }
//
//     public override void Process(TagHelperContext context, TagHelperOutput output)
//     {
//         output.TagName = "div";
//
//         var url = Page;
//         if (!string.IsNullOrWhiteSpace(Handler))
//             url += $"?handler={Handler}";
//
//         var verb = Method.ToLowerInvariant();
//
//         output.Attributes.SetAttribute($"hx-{verb}", url);
//         output.Attributes.SetAttribute("hx-trigger", Trigger);
//         output.Attributes.SetAttribute("hx-swap", Swap);
//
//         if (!string.IsNullOrWhiteSpace(Target))
//             output.Attributes.SetAttribute("hx-target", Target);
//
//         output.Content.SetHtmlContent(
//             "<div class=\"loading loading-spinner\"></div>"
//         );
//     }
// }