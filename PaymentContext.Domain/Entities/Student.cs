using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;

    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; private set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

    public void addSubscription(Subscription subscription)
    {
        var hasSubscriptionActive = false;

        foreach (var sub in Subscriptions)
        {
            if (sub.Active)
                hasSubscriptionActive = true;
        }

        //if (hasSubscriptionActive)
        //    AddNotification("Student.Subscriptions", "Já existe uma assinatura ativa");

        AddNotifications(new Contract<Notification>()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Já existe uma assinatura ativa")
            .IsLowerThan(0, subscription.Payments.Count, "Student.Subscriptions", "Essa assinatura não contém pagamentos")
        );
    }
}