using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsEmpty(FirstName, "Name.FirstName", "Nome é obrigatório")
            .IsEmpty(LastName, "Name.LastName", "Sobrenome é obrigatório")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}