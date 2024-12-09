using CM.API.Models;
using CM.API.Data;
using CM.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Threading.Tasks;

public class PaymentServiceTests
{
    private readonly PaymentService _paymentService;
    private readonly AppDbContext _context;

    public PaymentServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per test run
            .Options;

        _context = new AppDbContext(options);
        _paymentService = new PaymentService(_context);
    }

    [Fact]
    public async Task ProcessPayment_ShouldSucceed_WhenPaymentDetailsAreValid()
    {
        // Arrange
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "1234567812345678",
            CVV = "123",
            ExpiryDate = "12/25",
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        // Act
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert
        Assert.True(result);
        Assert.Single(_context.PaymentDetails);
    }

    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenCardNumberIsInvalid()
    {
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "1234", // Invalid card number
            CVV = "123",
            ExpiryDate = "12/25",
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        var result = await _paymentService.ProcessPayment(paymentDetails);

        Assert.False(result);
        Assert.Empty(_context.PaymentDetails);
    }

    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenCVVIsInvalid()
    {
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "1234567812345678",
            CVV = "12", // Invalid CVV
            ExpiryDate = "12/25",
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        var result = await _paymentService.ProcessPayment(paymentDetails);

        Assert.False(result);
        Assert.Empty(_context.PaymentDetails);
    }

    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenExpiryDateIsInvalid()
    {
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "1234567812345678",
            CVV = "123",
            ExpiryDate = "13/25", // Invalid expiry date
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        var result = await _paymentService.ProcessPayment(paymentDetails);

        Assert.False(result);
        Assert.Empty(_context.PaymentDetails);
    }

    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenAllDetailsAreInvalid()
    {
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "abcd1234",
            CVV = "12x",
            ExpiryDate = "20/99",
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        var result = await _paymentService.ProcessPayment(paymentDetails);

        Assert.False(result);
        Assert.Empty(_context.PaymentDetails);
    }
}
