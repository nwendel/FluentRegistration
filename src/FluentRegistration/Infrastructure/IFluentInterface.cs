using System.ComponentModel;

namespace FluentRegistration.Infrastructure;

[EditorBrowsable(EditorBrowsableState.Never)]
public interface IFluentInterface
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1024:Use properties where appropriate", Justification = "The intent is to hide the GetType() method from code completion")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The intent is to hide the GetType() method from code completion")]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    string? ToString();

    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object obj);
}
