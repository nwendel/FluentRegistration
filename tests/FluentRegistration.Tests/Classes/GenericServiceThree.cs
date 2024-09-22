namespace FluentRegistration.Tests.Classes;

public class GenericServiceThree<T> : GenericServiceTwo<T>, IAnotherInterface, IGenericInterface<T>
{
}
