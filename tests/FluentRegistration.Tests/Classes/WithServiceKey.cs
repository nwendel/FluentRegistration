namespace FluentRegistration.Tests.Classes;

public class WithServiceKey : IWithServiceKey, IServiceKeyAware
{
    public static object ServiceKey => "some-key";
}
