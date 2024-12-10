using CM.API.Models;
using CM.API.Data;
using CM.API.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context; // Database context to access the PaymentDetails table

    // Constructor to initialize the PaymentService with the database context
    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Processes the payment by validating the payment details, simulating the payment, and saving the payment details in the database.
    /// </summary>
    /// <param name="paymentDetails">The payment details to be processed.</param>
    /// <returns>A task that represents the asynchronous operation. Returns true if the payment is processed successfully, otherwise false.</returns>
    public async Task<bool> ProcessPayment(PaymentDetails paymentDetails)
    {
        // Validate the payment details
        if (!ValidatePaymentDetails(paymentDetails))
        {
            return false; // Return false if payment details are invalid
        }

        // Simulate payment processing by setting the payment date
        paymentDetails.PaymentDate = DateTime.Now;

        // Add payment details to the database and save changes
        _context.PaymentDetails.Add(paymentDetails);
        await _context.SaveChangesAsync();

        return true; // Return true indicating the payment was processed successfully
    }

    /// <summary>
    /// Validates the payment details including card number, CVV, and expiry date.
    /// </summary>
    /// <param name="paymentDetails">The payment details to validate.</param>
    /// <returns>True if the payment details are valid, otherwise false.</returns>
    private bool ValidatePaymentDetails(PaymentDetails paymentDetails)
    {
        // Validate card number (should be 16 digits long)
        if (string.IsNullOrEmpty(paymentDetails.CardNumber) || !Regex.IsMatch(paymentDetails.CardNumber, @"^\d{16}$"))
        {
            return false; // Invalid card number
        }

        // Validate CVV (should be 3 or 4 digits long)
        if (string.IsNullOrEmpty(paymentDetails.CVV) || !Regex.IsMatch(paymentDetails.CVV, @"^\d{3,4}$"))
        {
            return false; // Invalid CVV
        }

        // Validate expiry date (MM/YY format)
        if (string.IsNullOrEmpty(paymentDetails.ExpiryDate) || !Regex.IsMatch(paymentDetails.ExpiryDate, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$"))
        {
            return false; // Invalid expiry date
        }

        return true; // Return true if all validation checks pass
    }
}
