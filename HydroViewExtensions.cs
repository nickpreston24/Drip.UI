using CodeMechanic.Types;
using Hydro;
using Microsoft.AspNetCore.Html;

namespace Drip.UI;

public static class HydroViewExtensions
{
    public static bool IsEmpty(this HtmlString slot)
    {
        return slot != null && slot.Value.NotEmpty();
    }

    public static bool HasEmptySlot(this HydroView model, string slot_name = null)
    {
        if (slot_name.IsEmpty()) return false;
        var slot_html = model.Slot(slot_name);
        return slot_html != null && slot_html.Value.IsEmpty();
    }
}