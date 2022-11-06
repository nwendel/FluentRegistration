namespace FluentRegistration.Tests.Classes;

public class SimpleServiceServiceFactory
{
    private static SimpleService _simpleService = new SimpleService();

    public static SimpleService SimpleService => _simpleService;

    public ISimpleService CreateSimpleService()
    {
        return _simpleService;
    }
}
