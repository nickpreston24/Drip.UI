using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("drip-counter")]
public sealed class DripCounterTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("class", "card bg-base-200 p-4 w-64");
        output.Attributes.SetAttribute("hydro", "");
        output.Attributes.SetAttribute("x-data", "{ count: 0 }");

        output.Content.SetHtmlContent("""
            <div class="text-center space-y-2">
                <div class="text-lg font-bold">Drip Counter</div>
                <div class="text-3xl" x-text="count"></div>
                <button class="btn btn-primary"
                        @click="count++">
                    +1
                </button>
            </div>
        """);
    }
}