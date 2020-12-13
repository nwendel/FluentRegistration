namespace FluentRegistration.Internal
{
    public interface IWithServicesInitial :
        IFluentInterface
    {
        #region With Services

        IServiceSelector WithServices { get; }

        #endregion
    }
}
