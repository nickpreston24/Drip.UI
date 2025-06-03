using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Drip.UI.Pages.Extra;

public class Health : PageModel
{
    public void OnGet()
    {
        Console.WriteLine($"{nameof(Health)}.{nameof(OnGet)} loaded.");
    }
}