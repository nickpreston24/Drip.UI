using System.Globalization;
using System.Runtime.CompilerServices;
using CodeMechanic.Diagnostics;
using CodeMechanic.Razorhat;

namespace Drip.UI.Areas.DevTools.Pages;
// namespace Drip.UI.Areas.DevTools.Pages;

public class LogsIsland : RazorhatIsland
{
    public readonly SerilogLoggerName _loggerInfo;

    public LogsIsland(SerilogLoggerName loggerInfo)
    {
        loggerInfo.Dump(nameof(loggerInfo));
        // _loggerInfo = loggerInfo ?? throw new ArgumentNullException(nameof(loggerInfo));
    }

    public List<string> LogLines { get; private set; } = new();

    public async Task OnGet([CallerMemberName] string called_by = null)
    {
        Console.WriteLine($"{nameof(LogsIsland)}:>> LOGS ISLAND called by '{called_by}' ");
    }

    public override async Task Run()
    {
        Console.WriteLine($"{nameof(LogsIsland)}:>> LOGS ISLAND");

        if (_loggerInfo == null)
            return; // Do nothing if record not provided

        var dotFolder = _loggerInfo.dotfolder_name;
        var logName = _loggerInfo.log_name;

        // Construct log path
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var logPath = Path.Combine(home, $".dotnet/tools/.razorhat/{dotFolder}", $"{logName}.log");

        if (!System.IO.File.Exists(logPath))
            return;

        var lines = await System.IO.File.ReadAllLinesAsync(logPath);

        // Try to parse ISO timestamps at line start
        LogLines = lines
            .Select(l =>
            {
                var firstWord = l.Split(' ', 2).FirstOrDefault() ?? "";
                if (DateTime.TryParse(firstWord, null, DateTimeStyles.AssumeLocal, out var dt))
                    return (Date: dt, Line: l);
                return (Date: DateTime.MinValue, Line: l);
            })
            .OrderBy(t => t.Date)
            .Select(t => t.Line)
            .ToList();
    }
}
//
// // [HtmlTargetElement("serilog-table")]
// public class LogsIsland : PageModel
// {
//     public void OnGet()
//     {
//        
//     }
// }