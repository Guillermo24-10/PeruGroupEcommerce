using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PeruGroup.Ecommerce.Application.Interface.Infrastructure;
using PeruGroup.Ecommerce.Infrastructure.Notification.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PeruGroup.Ecommerce.Infrastructure.Notification
{
    public class NotificationSendGrid : INotification
    {
        private readonly ILogger<NotificationSendGrid> _logger;
        private readonly SendgridOptions _options;
        private readonly ISendGridClient _sendGridClient;

        public NotificationSendGrid(ILogger<NotificationSendGrid> logger, IOptions<SendgridOptions> options, ISendGridClient sendGridClient)
        {
            _logger = logger;
            _options = options.Value;
            _sendGridClient = sendGridClient;
        }

        public async Task<bool> SendEmailAsync(string subject, string body, CancellationToken cancellationToken = default)
        {
            SendGridMessage message = BuildMessage(subject, body);
            Response response = await _sendGridClient.SendEmailAsync(message, cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Email sent to {_options.ToAddress} at {response.Headers.Date}");
                return true;
            }

            _logger.LogError($"Failed to send email to {_options.ToAddress}. Status Code: {response.StatusCode}");
            return false;
        }

        private SendGridMessage BuildMessage(string subject, string body)
        {
            SendGridMessage msg = new SendGridMessage
            {
                From = new EmailAddress(_options.FromEmail, _options.FromUser),
                Subject = subject,
            };

            msg.AddContent(MimeType.Html, body);
            msg.AddTo(new EmailAddress(_options.ToAddress, _options.ToUser));

            if (_options.SandboxMode)
            {
                msg.MailSettings = new MailSettings { SandboxMode = new SandboxMode { Enable = true } };    
            }

            return msg;
        }
    }
}
