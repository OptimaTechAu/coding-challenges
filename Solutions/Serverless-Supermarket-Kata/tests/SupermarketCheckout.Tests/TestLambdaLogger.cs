using Amazon.Lambda.Core;

namespace SupermarketCheckout.Tests;

public class TestLambdaLogger : ILambdaLogger
{
    public void Log(string message) => Console.WriteLine(message);
    public void LogLine(string message) => Console.WriteLine(message);
    public void Log(string level, string message, params object[] args)
    {
        var formatted = args.Length > 0 ? FormatMessage(message, args) : message;
        Console.WriteLine($"[{level}] {formatted}");
    }

    private static string FormatMessage(string message, object[] args)
    {
        var result = message;
        foreach (var arg in args)
        {
            var index = result.IndexOf("{}", StringComparison.Ordinal);
            if (index >= 0)
            {
                result = string.Concat(result.AsSpan(0, index), arg?.ToString(), result.AsSpan(index + 2));
            }
        }
        return result;
    }
}
