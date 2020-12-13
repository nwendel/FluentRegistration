namespace FluentRegistration.Internal
{
    public interface ILifetimeSelector :
        IFluentInterface
    {
        #region Singleton

        IValidRegistration Singleton();

        #endregion

        #region Scoped

        IValidRegistration Scoped();

        #endregion

        #region Transient

        IValidRegistration Transient();

        #endregion
    }
}
