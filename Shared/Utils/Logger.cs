using System.Diagnostics;

namespace Shared.Utils;


public static class Logger
{
    public static string LogFilePath = @$"c:\Education\Datalagring-assignment\log.txt";

    public static void Log(string message)
    {
        try
        {
            var logMessage = $"{DateTime.Now} :: {message}";

            Debug.WriteLine(logMessage);

            using var sw = new StreamWriter(LogFilePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message}"); }
    }

    public static void Log(string message, LogTypes logType = LogTypes.Error)
    {
        try
        {
            var logMessage = $"{DateTime.Now} :: {logType} :: {message}";

            Debug.WriteLine(logMessage);

            using var sw = new StreamWriter(LogFilePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message}"); }
    }

    public static void Log(string message, string methodName = "", LogTypes logType = LogTypes.Error)
    {
        try
        {
            var logMessage = $"{DateTime.Now} :: {methodName} :: {logType} :: {message}";

            Debug.WriteLine(logMessage);

            using var sw = new StreamWriter(LogFilePath, true);
            sw.WriteLine(logMessage);
        }
        catch (Exception ex) { Debug.WriteLine($"{DateTime.Now} :: Logger.Log() :: {LogTypes.Error} :: {ex.Message}"); }
    }
}
