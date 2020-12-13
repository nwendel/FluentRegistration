namespace FluentRegistration.Internal
{
    public interface IServiceSelector :
        IFluentInterface
    {
        #region All Interfaces

        IWithServices AllInterfaces();

        #endregion

        #region Default Interface

        IWithServices DefaultInterface();

        #endregion

        #region Interface

        IWithServices Service<TService>();

        #endregion

        #region Self

        IWithServices Self();

        #endregion
    }
}
