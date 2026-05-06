using CodeMechanic.Types;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Drip.UI;

[HtmlTargetElement("island")]
public class IslandTagHelper : TagHelper
{
    [HtmlAttributeName("url")] public string Url { get; set; } = string.Empty;
    [HtmlAttributeName("event")] public IslandEvents Event { get; set; } = IslandEvents.Intersect;

    public string Page { get; set; } = string.Empty;
    public string? Handler { get; set; } = string.Empty;

    public string Method { get; set; } = "get";
    public string Target { get; set; } = string.Empty;

    public IslandSwaps Swap { get; set; } = IslandSwaps.InnerHTML;

    public bool debug { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (debug)
            Console.WriteLine($"[IslandTagHelper] → <{context.TagName}> | Url='{Url}' | Page='{Page}' | Event={Event}");

        // ─────────────────────────────────────────────────────────────
        // CRITICAL GUARD — stops the infinite reload loop instantly
        // If we have no URL and no Page, this is a short tag (<badge />)
        // that fell through to the base class → just render it as plain HTML.
        // ─────────────────────────────────────────────────────────────
        if (Url.IsEmpty() && Page.IsEmpty())
        {
            if (debug)
                Console.WriteLine(
                    $"[IslandTagHelper] → EMPTY URL fallback: rendering <{context.TagName}> as plain div");
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            var emptyContent = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(emptyContent);
            return;
        }

        output.TagName = "div";

        if (Url.IsEmpty() && Page.NotEmpty())
        {
            Url = Page;
            if (Handler.NotEmpty())
                Url += $"?handler={Handler}";
        }

        var @event = Event switch
        {
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

        // Only wire htmx when we actually have a URL
        if (Url.NotEmpty())
        {
            output.Attributes.SetAttribute($"hx-{verb}", Url);
            output.Attributes.SetAttribute("hx-trigger", @event);
            output.Attributes.SetAttribute("hx-swap", swap);

            if (Target.NotEmpty())
                output.Attributes.SetAttribute("hx-target", Target);
        }

        var childContent = await output.GetChildContentAsync();
        output.Content.SetHtmlContent(childContent);
        output.TagMode = TagMode.StartTagAndEndTag;
    }
}

/*
 /// MY OG IMPLEMENTATION

[HtmlTargetElement("island")]
public class IslandTagHelper : TagHelper
{
    [HtmlAttributeName("url")] public string Url { get; set; } = string.Empty;
    [HtmlAttributeName("event")] public IslandEvents Event { get; set; } = IslandEvents.Intersect;

    public string Page { get; set; } = String.Empty;
    public string? Handler { get; set; } = String.Empty;

    public string Method { get; set; } = "get"; // todo: make an enum for this.
    public string Target { get; set; } = String.Empty;

    public IslandSwaps Swap { get; set; } = IslandSwaps.InnerHTML;

    public bool debug { get; set; } = true;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (debug) Console.WriteLine(nameof(ProcessAsync) + $" - event: {Event} - url: {Url}");

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
            // IslandEvents.Load => "load",
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

        if (debug)
        {
            Console.WriteLine($"url:>> {Url}");
            Console.WriteLine($"url:>> {Event}");
            Console.WriteLine($"url:>> {swap}");
        }

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
*/
