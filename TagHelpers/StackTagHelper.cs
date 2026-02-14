using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("stack")]
public class StackTagHelper : TagHelper
{
    private readonly HtmlEncoder encoder;

    public StackTagHelper(HtmlEncoder encoder)
    {
        this.encoder = encoder;
    }

    public int? Gap { get; set; } = 2;

    public string? Align { get; set; } = "center"; // start, center, end, stretch
    public string? Justify { get; set; } // start, center, end, between, around, evenly


    // TODO: encoder hates spaces in classnames, so we need to fix that.

    // [HtmlAttributeName("class")]
    // public string? Class { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.AddClass("flex", encoder);
        output.AddClass("flex-col", encoder);

        if (Gap.HasValue)
            output.AddClass($"gap-{Gap}", encoder);

        if (!string.IsNullOrWhiteSpace(Align))
            output.AddClass($"items-{Align}", encoder);

        if (!string.IsNullOrWhiteSpace(Justify))
            output.AddClass($"justify-{Justify}", encoder);

        // TODO: encoder hates spaces in classnames, so we need to fix that.
        // if (!string.IsNullOrWhiteSpace(Class))
        //     output.AddClass(Class, _encoder);

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);
    }
}