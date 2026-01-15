using System.ComponentModel.DataAnnotations;
using CodeMechanic.Types;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI.TagHelpers;

[HtmlTargetElement("island")]
public class IslandTagHelper : TagHelper
{
    [HtmlAttributeName("url")] public string Url { get; set; } = string.Empty;
    [HtmlAttributeName("event")] public IslandEvents Event { get; set; } = IslandEvents.Load;

    public string Page { get; set; } = String.Empty;
    public string? Handler { get; set; } = String.Empty;

    public string Method { get; set; } = "get"; // todo: make an enum for this.
    public string Target { get; set; } = String.Empty;

    public IslandSwaps Swap { get; set; } = IslandSwaps.OuterHTML;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        Console.WriteLine(nameof(ProcessAsync) + $" - event: {Event} - url: {Url}");
        // Changing the tag name to "div"
        output.TagName = "div";

        if (Url.IsEmpty() && Page.NotEmpty())
        {
            Url = Page;
            if (!string.IsNullOrWhiteSpace(Handler))
                Url += $"?handler={Handler}";
        }

        var @event = Event switch
        {
            IslandEvents.Load => "load",
            IslandEvents.Revealed => "revealed",
            IslandEvents.Intersect => "intersect once",
            _ => "load"
        };

        var swap = Swap switch
        {
            IslandSwaps.OuterHTML => "outerHTML",
            IslandSwaps.InnerHTML => "innerHTML",
            _ => "outerHTML"
        };

        var verb = Method.ToLowerInvariant();

        output.Attributes.SetAttribute($"hx-{verb}", Url);
        output.Attributes.SetAttribute("hx-trigger", @event);
        output.Attributes.SetAttribute("hx-swap", swap);
        if (!string.IsNullOrWhiteSpace(Target))
            output.Attributes.SetAttribute("hx-target", Target);

        // Retrieve the inner content
        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);

        // Ensuring the tag is not self-closing
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}