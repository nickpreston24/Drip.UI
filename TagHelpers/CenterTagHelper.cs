using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("center")]
public class CenterTagHelper : TagHelper
{
    private readonly HtmlEncoder _encoder;

    public CenterTagHelper(HtmlEncoder encoder)
    {
        _encoder = encoder;
    }

    [HtmlAttributeName("class")]
    public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.AddClass("flex", _encoder);
        output.AddClass("items-center", _encoder);
        output.AddClass("justify-center", _encoder);

        if (!string.IsNullOrWhiteSpace(Class))
            output.AddClass(Class, _encoder);

        var content = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(content);
    }
}