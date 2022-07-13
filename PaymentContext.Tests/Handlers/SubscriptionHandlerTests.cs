using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();

        command.FirstName = "Bruce";
        command.LastName = "Wayne";
        command.Document = "99999999999";
        command.Address = "";
        command.Email = "teste2@teste.com";

        command.BarCode = "123456789";
        command.BoletoNumber = "123456789";

        command.PaymentNumber = "123";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 50;
        command.TotalPaid = 50;
        command.Payer = "Wayne";
        command.PayerDocument = "12345678901";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "bat@man.com";

        command.Street = "Rua";
        command.Number = "123";
        command.Neighborhood = "Bairro";
        command.City = "Gotan";
        command.State = "SP";
        command.Country = "BR";
        command.ZipCode = "12345-000";

        handler.Handle(command);

        Assert.AreEqual(false, handler.IsValid);
    }
}
