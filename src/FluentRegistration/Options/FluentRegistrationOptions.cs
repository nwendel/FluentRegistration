namespace FluentRegistration.Options;

public class FluentRegistrationOptions
{
    public static readonly FluentRegistrationOptions Default = new FluentRegistrationOptions
    {
        MultipleRegistrationsBehavior = MultipleRegistrationsBehavior.Ignore,
        RegistrationsWithoutServicesBehavior = RegistrationsWithoutServicesBehavior.Ignore,
    };

    public MultipleRegistrationsBehavior MultipleRegistrationsBehavior { get; set; }

    public RegistrationsWithoutServicesBehavior RegistrationsWithoutServicesBehavior { get; set; }
}
