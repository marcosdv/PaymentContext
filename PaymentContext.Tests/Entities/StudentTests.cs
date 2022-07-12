using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Email _email;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12345678901", Domain.Enums.EDocumentType.CPF);
        _email = new Email("bat@man.com");
        _address = new Address("Rua 1", "1234", "Bairro", "Gotam", "SP", "BR", "12345-000");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddSeconds(5), 10, 10, "Wayne", _document, _address, _email);
        _subscription.AddPayments(payment);
        _student.addSubscription(_subscription);
        _student.addSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {
        _student.addSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenHadNoActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddSeconds(5), 10, 10, "Wayne", _document, _address, _email);
        _subscription.AddPayments(payment);
        _student.addSubscription(_subscription);

        Assert.IsFalse(_student.IsValid);
    }
}