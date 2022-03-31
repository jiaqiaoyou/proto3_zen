using System;

public static class Log
{
    private const string WarningPrefix = "[Warning]";
    private const string InfoPrefix = "[Info]";
    private const string ErrorPrefix = "[Error";

    public static void Info(string log, int ident = 0)
    {
        var identStr = StringUtil.ReplicateString("\t", ident);
        log = $"{DateTime.Now:T}{InfoPrefix} {identStr}{log}";
        Console.WriteLine(log, System.Drawing.Color.Green);
    }

    public static void Warning(string log, int ident = 0)
    {
        var identStr = StringUtil.ReplicateString("\t", ident);
        log = $"{DateTime.Now:T}{WarningPrefix} {identStr}{log}";
        Console.WriteLine(log, System.Drawing.Color.Yellow);
    }

    public static void Error(string log, int ident = 0)
    {
        var identStr = StringUtil.ReplicateString("\t", ident);
        log = $"{DateTime.Now:T}{ErrorPrefix} {identStr}{log}";
        Console.WriteLine(log, System.Drawing.Color.Red);
    }
}
