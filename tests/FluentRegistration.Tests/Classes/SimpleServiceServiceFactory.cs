namespace FluentRegistration.Tests.Classes;

public class SimpleServiceServiceFactory
{
    private static readonly SimpleService _simpleService = new();

    public static SimpleService SimpleService => _simpleService;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Just for testing")]
    public ISimpleService CreateSimpleService()
    {
        return _simpleService;
    }
}
