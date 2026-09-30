using System.Globalization;
using CodeMechanic.Logging;
using CodeMechanic.Razorhat;

namespace Drip.UI.Areas.DevTools.Pages;

public class LogsIsland : RazorhatIsland
{
    public readonly SerilogLoggerName _loggerInfo;

    public LogsIsland(SerilogLoggerName loggerInfo)
    {
        _loggerInfo = loggerInfo;
    }

    public List<string> LogLines { get; private set; } = new();

    public async Task OnGet()
    {
        await Run();
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
        var dir = Path.Combine(home, ".dotnet", "tools", dotFolder);
        if (!Directory.Exists(dir))
            return;

        var exact = Path.Combine(dir, $"{logName}.log");
        var logPath = System.IO.File.Exists(exact)
            ? exact
            : Directory.GetFiles(dir, $"{logName}*.log")
                .OrderByDescending(System.IO.File.GetLastWriteTimeUtc)
                .FirstOrDefault();

        if (string.IsNullOrEmpty(logPath))
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