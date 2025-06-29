using System.Text.RegularExpressions;
using Hydro;

namespace Drip.UI;

public class DaisyLink : HydroView
{
    // todo: replace with better url check
    private static Regex url_regex = new Regex(
        @"https:(www)?//\w+\.\w+/.*$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );

    public bool is_valid => url_regex.IsMatch(href);
    public string href { get; set; } = string.Empty;
}