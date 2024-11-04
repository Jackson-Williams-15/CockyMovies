using CM.API.Services;
namespace CM.API.Interfaces;
using CM.API.Models;
public interface IPaymentService
{
    Task<bool> ProcessPayment(PaymentDetails paymentDetails);
}