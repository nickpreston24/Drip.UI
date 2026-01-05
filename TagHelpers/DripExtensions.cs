using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;

// using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;


public static class DripExtensions
{
    // public static WebApplicationBuilder UseDrip(this WebApplicationBuilder builder)
    // {
    //     builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
    //
    //     builder.Services.AddTransient<ITagHelperComponent, DripHydroTagHelperComponent>();
    //     // builder.Services.AddTransient<ITagHelperComponent, DripAlpineTagHelperComponent>();
    //
    //     return builder;
    // }

    public static WebApplicationBuilder UseDrip(this WebApplicationBuilder builder)
    {
        // builder.Services.AddRazorPages();

        builder.Services.AddTransient<ITagHelperComponent, DripHydroTagHelperComponent>();
        // builder.Services.AddTransient<ITagHelperComponent, DripCounterTagHelper>();

        return builder;
    }
}


public sealed class DripHydroTagHelperComponent : ITagHelperComponent
{
    public void Init(TagHelperContext context)
    {
        // throw new NotImplementedException();
    }

    public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        // throw new NotImplementedException();
    }

    public int Order => 0;

    public void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (output.TagName == "div" &&
            context.AllAttributes.ContainsName("hydro"))
        {
            output.Attributes.SetAttribute("x-data", "hydro");
        }
    }
}