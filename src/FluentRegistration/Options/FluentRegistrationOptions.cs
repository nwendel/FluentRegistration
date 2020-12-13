namespace FluentRegistration.Options
{
    public class FluentRegistrationOptions
    {
        #region Default

        public static readonly FluentRegistrationOptions Default = new FluentRegistrationOptions
        {
            MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore,
            RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore,
        };

        #endregion

        #region Properties

        public MultipleRegistrationsBehavior MultipleRegistrationsBehavior { get; set; }

        public RegistrationsWithoutServicesBehavior RegistrationsWithoutServicesBehavior { get; set; }

        #endregion
    }
}
