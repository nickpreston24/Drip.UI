using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("grid")]
public class GridTagHelper : TagHelper
{
    private readonly HtmlEncoder _encoder;

    // TODO: the encoder hates spaces in classnames - this will need smart Extraction and validation.
    // [HtmlAttributeName("class")] public string? Class { get; set; }

    public GridTagHelper(HtmlEncoder encoder)
    {
        _encoder = encoder;
    }

    public int Rows { get; set; } = -1;
    public int Cols { get; set; } = -1;
    public int Gap { get; set; } = -1;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // Change tag to div
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        // Always add base grid class
        output.AddClass("grid", _encoder);

        // TODO: the encoder hates spaces in classnames - this will need smart Extraction and validation.
        // if (!string.IsNullOrWhiteSpace(Class))
        //     output.AddClass(Class, _encoder);

        if (Rows > -1)
            output.AddClass($"grid-rows-{Rows}", _encoder);

        if (Cols > -1)
            output.AddClass($"grid-cols-{Cols}", _encoder);

        if (Gap > -1)
            output.AddClass($"gap-{Gap}", _encoder);

        // Preserve child content
        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);
    }
}