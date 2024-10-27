namespace FluentRegistration;

public interface IServiceKeyAware
{
    public static abstract object ServiceKey { get; }
}
