using CM.API.Models;

public interface IEmailService
{
    /// <summary>
    /// Sends an email with the specified subject and body to the specified recipient.
    /// General purpose email
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmail(string to, string subject, string body);

    /// <summary>
    /// Sends an email of the specified type to the specified recipient.
    /// email for specified type
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="emailType">The type of email to send.</param>
    /// <param name="data">The data to include in the email.</param>
    /// <param name="user">The user associated with the email.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendEmail(string to, EmailType emailType, object data, User user);
}
