using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CM.API.Models;
public class EmailSettings
{
    public string? SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string? SenderName { get; set; }
    public string? SenderEmail { get; set; }
    public string? SenderPassword { get; set; }
}

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmail(string to, string subject, string body)
    {
        var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail ?? throw new ArgumentNullException(nameof(_emailSettings.SenderEmail)), _emailSettings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }

    public async Task SendEmail(string to, EmailType emailType, object data, User user)
    {
        string subject = string.Empty;
        string body = string.Empty;

        // Switch case for which email type to send
        switch (emailType)
        {
            case EmailType.OrderReceipt:
                var orderReceipt = data as OrderReceiptDto;
                subject = "Your Order Receipt";
                body = GenerateOrderReceiptEmailBody(orderReceipt, user);
                break;
            case EmailType.Verification:
                subject = "Verify Your Email";
                body = GenerateVerificationEmailBody(user);
                break;
        }

        await SendEmail(to, subject, body);
    }
    
    /// <summary>
    /// Creates the email body for an order receipt.
    /// </summary>
    /// <param name="orderReceipt">The order receipt data.</param>
    /// <param name="user">The user associated with the order.</param>
    /// <returns>The generated email body.</returns>
    private string GenerateOrderReceiptEmailBody(OrderReceiptDto orderReceipt, User user)
    {
        var emailBody = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                margin: 0;
                padding: 0;
                background-color: #f9f9f9;
            }}
            .email-container {{
                max-width: 600px;
                margin: 20px auto;
                padding: 20px;
                background-color: #ffffff;
                border-radius: 10px;
                box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            }}
            .header {{
                font-size: 20px;
                font-weight: bold;
                text-align: center;
                margin-bottom: 20px;
                color: #007BFF;
            }}
            .order-details {{
                margin-bottom: 20px;
            }}
            .order-details h3 {{
                font-size: 18px;
                margin-bottom: 10px;
                color: #444;
            }}
            .ticket {{
                margin-bottom: 10px;
                padding: 10px;
                border-bottom: 1px solid #e0e0e0;
            }}
            .footer {{
                text-align: center;
                margin-top: 20px;
                font-size: 12px;
                color: #888;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='header'>
                Thank you for your order, {user?.Username}!
            </div>
            <div class='order-details'>
                <h3>Order Summary</h3>
                <p><strong>Order ID:</strong> {orderReceipt.OrderId}</p>
                <p><strong>Processed Date:</strong> {orderReceipt.ProcessedDate}</p>
                <p><strong>Total Price:</strong> ${orderReceipt.TotalPrice:F2}</p>
            </div>
            <div class='tickets'>
                <h3>Tickets</h3>";

        foreach (var ticket in orderReceipt.Tickets)
        {
            emailBody += $@"
                <div class='ticket'>
                    <p><strong>Movie:</strong> {ticket.MovieTitle}</p>
                    <p><strong>Showtime:</strong> {ticket.ShowtimeStartTime}</p>
                    <p><strong>Quantity:</strong> {ticket.Quantity}</p>
                    <p><strong>Price per Ticket:</strong> ${ticket.Price:F2}</p>
                    <p><strong>Total Price:</strong> ${(ticket.Price * ticket.Quantity):F2}</p>
                </div>";
        }
        return emailBody;
    }

    /// <summary>
    /// Generates the email body for email verification.
    /// </summary>
    /// <param name="user">The user to verify.</param>
    /// <returns>The generated email body.</returns>
    private string GenerateVerificationEmailBody(User user)
    {
        return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    line-height: 1.6;
                    color: #333;
                    margin: 0;
                    padding: 0;
                    background-color: #f9f9f9;
                }}
                .email-container {{
                    max-width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    background-color: #ffffff;
                    border-radius: 10px;
                    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
                }}
                .header {{
                    font-size: 20px;
                    font-weight: bold;
                    text-align: center;
                    margin-bottom: 20px;
                }}
                .footer {{
                    text-align: center;
                    margin-top: 20px;
                    font-size: 12px;
                    color: #888;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <div class='header'>
                    Verify Your Email, {user.Username}!
                </div>
                <div class='content'>
                    <p>Thank you for registering. Please verify your email by clicking the link below:</p>
                    <p><a href='https://example.com/verify?user={user.Id}'>Verify Email</a></p>
                </div>
                <div class='footer'>
                    If you did not register, please ignore this email.
                </div>
            </div>
        </body>
        </html>";
    }
}