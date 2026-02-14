using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("box")]
public class BoxTagHelper : TagHelper
{
    private readonly HtmlEncoder _encoder;

    public BoxTagHelper(HtmlEncoder encoder)
    {
        _encoder = encoder;
    }

    public int? Padding { get; set; }
    public string? Rounded { get; set; }
    public string? Shadow { get; set; }

    [HtmlAttributeName("class")]
    public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        if (Padding.HasValue)
            output.AddClass($"p-{Padding}", _encoder);

        if (!string.IsNullOrWhiteSpace(Rounded))
            output.AddClass($"rounded-{Rounded}", _encoder);

        if (!string.IsNullOrWhiteSpace(Shadow))
            output.AddClass($"shadow-{Shadow}", _encoder);

        if (!string.IsNullOrWhiteSpace(Class))
            output.AddClass(Class, _encoder);

        var content = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(content);
    }
}