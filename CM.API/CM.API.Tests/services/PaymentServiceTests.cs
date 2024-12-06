using Moq;
using NUnit.Framework;
using CM.API.Models;
using CM.API.Data;
using CM.API.Interfaces;
using System.Threading.Tasks;

namespace CM.API.Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private Mock<AppDbContext> _mockDbContext;
        private Mock<IPaymentService> _mockPaymentService;
        private PaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<AppDbContext>();
            _mockPaymentService = new Mock<IPaymentService>();
            _paymentService = new PaymentService(_mockDbContext.Object);
        }

        [Test]
        public async Task ProcessPayment_ValidPaymentDetails_ReturnsTrue()
        {
            // Arrange
            var validPaymentDetails = new PaymentDetails
            {
                CardNumber = "1234567812345678",
                CVV = "123",
                ExpiryDate = "12/23"
            };

            _mockDbContext.Setup(db => db.PaymentDetails.Add(It.IsAny<PaymentDetails>()));
            _mockDbContext.Setup(db => db.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _paymentService.ProcessPayment(validPaymentDetails);

            // Assert
            Assert.IsTrue(result);
            _mockDbContext.Verify(db => db.PaymentDetails.Add(It.IsAny<PaymentDetails>()), Times.Once);
            _mockDbContext.Verify(db => db.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task ProcessPayment_InvalidCardNumber_ReturnsFalse()
        {
            // Arrange
            var invalidCardNumberPaymentDetails = new PaymentDetails
            {
                CardNumber = "12345", // Invalid card number
                CVV = "123",
                ExpiryDate = "12/23"
            };

            // Act
            var result = await _paymentService.ProcessPayment(invalidCardNumberPaymentDetails);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ProcessPayment_InvalidCVV_ReturnsFalse()
        {
            // Arrange
            var invalidCVVPaymentDetails = new PaymentDetails
            {
                CardNumber = "1234567812345678",
                CVV = "12", // Invalid CVV
                ExpiryDate = "12/23"
            };

            // Act
            var result = await _paymentService.ProcessPayment(invalidCVVPaymentDetails);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task ProcessPayment_InvalidExpiryDate_ReturnsFalse()
        {
            // Arrange
            var invalidExpiryDatePaymentDetails = new PaymentDetails
            {
                CardNumber = "1234567812345678",
                CVV = "123",
                ExpiryDate = "13/23" // Invalid expiry date (13 is not a valid month)
            };

            // Act
            var result = await _paymentService.ProcessPayment(invalidExpiryDatePaymentDetails);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
