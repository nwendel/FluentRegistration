namespace FluentRegistration.Internal
{
    public interface ILifetime :
        IValidRegistration
    {
        #region Lifetime

        ILifetimeSelector Lifetime { get; }

        #endregion
    }
}
