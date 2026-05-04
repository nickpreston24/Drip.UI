using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

// Drip.UI/TagHelpers/EditableFieldIslandSample.cs  (or let the generator make these)
[HtmlTargetElement("editable-field-sample")]
public class EditableFieldIslandSample : IslandTagHelper
{
    public EditableFieldIslandSample()
    {
        Event = IslandEvents.Intersect; // loads when scrolled into view
        Swap = IslandSwaps.OuterHTML;
        Url = "/editable-field"; // your handler endpoint
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetHtmlContent("""
                                      <div class="card bg-base-200 p-6">
                                          <div class="flex items-center gap-3">
                                              <span class="font-medium">Name:</span>
                                              <input
                                                  type="text"
                                                  id="name-input"
                                                  class="input input-bordered flex-1"
                                                  value="John Doe"
                                                  hx-post="/editable-field"
                                                  hx-trigger="blur"
                                                  hx-swap="outerHTML" />
                                          </div>
                                      </div>
                                      """);

        await base.ProcessAsync(context, output);
    }
}
