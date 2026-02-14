using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("spacer")]
public class SpacerTagHelper : TagHelper
{
    private readonly HtmlEncoder _encoder;

    public SpacerTagHelper(HtmlEncoder encoder)
    {
        _encoder = encoder;
    }

    // TODO: the encoder hates spaces in classnames - this will need smart Extraction and validation.

    // [HtmlAttributeName("class")]
    // public string? Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.AddClass("grow", _encoder);

        // if (!string.IsNullOrWhiteSpace(Class))
        //     output.AddClass(Class, _encoder);
    }
}