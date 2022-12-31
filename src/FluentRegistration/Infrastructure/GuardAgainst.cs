using System.Runtime.CompilerServices;

namespace FluentRegistration.Infrastructure;

internal static class GuardAgainst
{
    public static void Null<T>(T argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        where T : class
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrEmpty<T>(IEnumerable<T> argument, [CallerArgumentExpression("argument")] string? argumentName = null)
        where T : class
    {
        if (argument == null || argument.None())
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrWhiteSpace(string argument, [CallerArgumentExpression("argument")] string? argumentName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}
