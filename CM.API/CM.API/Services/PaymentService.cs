using CM.API.Models;
using CM.API.Data;
using CM.API.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ProcessPayment(PaymentDetails paymentDetails)
    {
        if (!ValidatePaymentDetails(paymentDetails))
        {
            return false;
        }

        // Simulate payment processing
        paymentDetails.PaymentDate = DateTime.Now;
        _context.PaymentDetails.Add(paymentDetails);
        await _context.SaveChangesAsync();

        return true;
    }

    private bool ValidatePaymentDetails(PaymentDetails paymentDetails)
    {
        // Validate card number (16 digits)
        if (!Regex.IsMatch(paymentDetails.CardNumber, @"^\d{16}$"))
        {
            return false;
        }

        // Validate CVV (3-4 digits)
        if (!Regex.IsMatch(paymentDetails.CVV, @"^\d{3,4}$"))
        {
            return false;
        }

        // Validate expiry date (MM/YY format)
        if (!Regex.IsMatch(paymentDetails.ExpiryDate, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$"))
        {
            return false;
        }

        return true;
    }
}