using CM.API.Models;

namespace CM.API.Interfaces
{
    /// <summary>
    /// Defines the contract for processing payments.
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Processes a payment based on the provided payment details.
        /// </summary>
        /// <param name="paymentDetails">The payment details, including card information and amount.</param>
        /// <returns>A task that represents the asynchronous operation. Returns true if the payment is successfully processed, otherwise false.</returns>
        Task<bool> ProcessPayment(PaymentDetails paymentDetails);
    }
}
