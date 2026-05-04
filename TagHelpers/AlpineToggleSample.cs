using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("alpine-toggle-sample")]
public class AlpineToggleSample : TagHelper
{
    private readonly HtmlEncoder encoder;

    public AlpineToggleSample(HtmlEncoder encoder)
    {
        this.encoder = encoder;
    }

    // TODO: encoder hates spaces in classnames, so we need to fix that.

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;

        string alpineExample = """
                               <div x-data="{ open: false }">
                                   <button
                                       class="btn btn-primary"
                                       @click="open = !open">
                                       Toggle Content
                                   </button>

                                   <div x-show="open"
                                        class="mt-4 p-4 bg-base-200 rounded-box">
                                       <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit.
                                          Alias doloribus, et ipsum itaque libero neque nulla praesentium
                                          voluptas? Ab aliquid distinctio labore laboriosam laborum quia
                                          quo sed sint ullam veritatis?</p>
                                   </div>
                               </div>
                               """;

        // This is the correct way when you want to hard-code the content
        output.Content.SetHtmlContent(alpineExample);

        // If you ever want to support child content inside the tag, do this instead:
        // var child = await output.GetChildContentAsync();
        // output.Content.SetHtmlContent(alpineExample + child.GetContent());
    }
}



