// Import necessary namespaces for the test class
using CM.API.Models; // Models for database entities
using CM.API.Data; // Data access layer
using CM.API.Services; // Business logic services
using Microsoft.EntityFrameworkCore; // Entity Framework Core for in-memory database
using Xunit; // Testing framework
using System; // For DateTime
using System.Threading.Tasks; // For async methods

// Test class for PaymentService
public class PaymentServiceTests
{
    private readonly PaymentService _paymentService; // Service under test
    private readonly AppDbContext _context; // In-memory database context

    // Constructor initializes in-memory database and service
    public PaymentServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB per test run
            .Options;

        _context = new AppDbContext(options); // Initialize database context
        _paymentService = new PaymentService(_context); // Create service instance
    }

    // Test to ensure payment processing succeeds with valid details
    [Fact]
    public async Task ProcessPayment_ShouldSucceed_WhenPaymentDetailsAreValid()
    {
        // Arrange: Set up valid payment details
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

        // Act: Call the service to process payment
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert: Verify payment succeeded and was saved in the database
        Assert.True(result); // Ensure success
        Assert.Single(_context.PaymentDetails); // Ensure only one record was added
    }

    // Test to ensure payment fails with an invalid card number
    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenCardNumberIsInvalid()
    {
        // Arrange: Invalid card number
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

        // Act: Attempt payment
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert: Ensure payment failed and nothing was saved
        Assert.False(result); // Ensure failure
        Assert.Empty(_context.PaymentDetails); // Ensure no records were added
    }

    // Test to ensure payment fails with an invalid CVV
    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenCVVIsInvalid()
    {
        // Arrange: Invalid CVV
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

        // Act: Attempt payment
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert: Ensure payment failed and nothing was saved
        Assert.False(result); // Ensure failure
        Assert.Empty(_context.PaymentDetails); // Ensure no records were added
    }

    // Test to ensure payment fails with an invalid expiry date
    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenExpiryDateIsInvalid()
    {
        // Arrange: Invalid expiry date
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

        // Act: Attempt payment
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert: Ensure payment failed and nothing was saved
        Assert.False(result); // Ensure failure
        Assert.Empty(_context.PaymentDetails); // Ensure no records were added
    }

    // Test to ensure payment fails with completely invalid details
    [Fact]
    public async Task ProcessPayment_ShouldFail_WhenAllDetailsAreInvalid()
    {
        // Arrange: Completely invalid payment details
        var paymentDetails = new PaymentDetails
        {
            CardNumber = "abcd1234", // Invalid card number
            CVV = "12x", // Invalid CVV
            ExpiryDate = "20/99", // Invalid expiry date
            Amount = 100.00m,
            PaymentDate = DateTime.UtcNow,
            CardHolderName = "John Doe",
            PaymentMethod = "CreditCard"
        };

        // Act: Attempt payment
        var result = await _paymentService.ProcessPayment(paymentDetails);

        // Assert: Ensure payment failed and nothing was saved
        Assert.False(result); // Ensure failure
        Assert.Empty(_context.PaymentDetails); // Ensure no records were added
    }
}
