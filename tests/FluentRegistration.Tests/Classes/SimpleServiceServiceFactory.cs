namespace FluentRegistration.Tests.Classes;

public class SimpleServiceServiceFactory
{
    private static readonly SimpleService _simpleService = new();

    public static SimpleService SimpleService => _simpleService;

    public ISimpleService CreateSimpleService()
    {
        return _simpleService;
    }
}
