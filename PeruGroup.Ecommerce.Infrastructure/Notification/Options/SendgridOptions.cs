namespace PeruGroup.Ecommerce.Infrastructure.Notification.Options
{
    public class SendgridOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromUser { get; set; } = string.Empty;
        public bool SandboxMode { get; set; } = false;
        public string ToAddress { get; set; } = string.Empty;
        public string ToUser { get; set; } = string.Empty;
    }
}
