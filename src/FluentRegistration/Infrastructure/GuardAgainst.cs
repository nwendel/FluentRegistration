using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentRegistration.Infrastructure;

public static class GuardAgainst
{
    public static void Null<T>(T argument, string argumentName)
        where T : class
    {
        if (argument == null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrEmpty<T>(IEnumerable<T> argument, string argumentName)
        where T : class
    {
        if (argument == null || !argument.Any())
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    public static void NullOrWhiteSpace(string argument, string argumentName)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentNullException(argumentName);
        }
    }
}
