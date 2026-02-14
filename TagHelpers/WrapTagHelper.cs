using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("wrap")]
public class WrapTagHelper : TagHelper
{
    private readonly HtmlEncoder _encoder;

    public WrapTagHelper(HtmlEncoder encoder)
    {
        _encoder = encoder;
    }

    public int? Gap { get; set; }

    // TODO: the encoder hates spaces in classnames - this will need smart Extraction and validation.
    // [HtmlAttributeName("class")]
    // public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.AddClass("flex", _encoder);
        output.AddClass("flex-wrap", _encoder);

        if (Gap.HasValue)
            output.AddClass($"gap-{Gap}", _encoder);
        
        // TODO: the encoder hates spaces in classnames - this will need smart Extraction and validation.
        // if (!string.IsNullOrWhiteSpace(Class))
        //     output.AddClass(Class, _encoder);

        var content = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(content);
    }
}