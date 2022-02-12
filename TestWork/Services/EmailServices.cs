using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TestWork.DBContexts;
using TestWork.MailConfig;
using TestWork.Models;
using TestWork.Reposirory;

namespace TestWork.Services
{
    /// <summary>
    /// Service for sending a message to the email.
    /// </summary>
    public class EmailServices : IEmailServices
    {
        public readonly IEmailRepository _emailRepository;

        private readonly MailConfigConfiguration _config;

        public EmailServices(IEmailRepository emailRepository, IOptions<MailConfigConfiguration> config)
        {
         
            _emailRepository = emailRepository;
            _config = config.Value;
        }

        /// <summary>
        /// Publishing message to emails. 
        /// </summary>
        /// <param name="message">Body request is Json format</param>
        /// <returns></returns>
        public async Task SendAndLogEmail(Message message)
        {
            MimeMessage newMessage = new MimeMessage();
            newMessage.From.Add(new MailboxAddress("EmailLog", _config.SmtpUsername));

            newMessage.Subject = message.Subject;
            newMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = message.Body
            };

            List<EmailLog> emailLogsList = new List<EmailLog>();

            foreach (var recipient in message.Recipients)
            {
                SmtpClient client = new SmtpClient();
                EmailLog emailLog = new EmailLog
                {
                    Subject = message.Subject,
                    Body = message.Body,
                    Recipients = recipient,
                    EmailSent = DateTime.Now,
                    Result = "OK",
                    FailedMessage = ""
                };

                try
                {
                    await client.ConnectAsync(_config.SmtpServer, _config.SmtpPort, _config.SmtpUseSSL);
                    await client.AuthenticateAsync(_config.SmtpUsername, _config.SmtpPassword);
                    newMessage.To.Add(MailboxAddress.Parse(recipient));
                    await client.SendAsync(newMessage);

                    var failedMessage = GetDeliveryStatusNotifications(newMessage);
                    if (failedMessage != null)
                        throw new Exception(failedMessage);
                }
                catch (Exception ex)
                {
                    emailLog.Result = "Failed";
                    emailLog.FailedMessage = ex.Message;
                }
                finally
                {
                    //await _emailRepository.AddLog(emailLog);
                    emailLogsList.Add(emailLog);

                    client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
            await _emailRepository.AddLog(emailLogsList);
        }

        /// <summary>
        /// Checking the status of sending a message
        /// </summary>
        /// <param name="mes">Message object</param>
        /// <returns>Status</returns>
        /// 
        private string? GetDeliveryStatusNotifications(MimeMessage mes)
        {
            if (!(mes.Body is MultipartReport report) || report.ReportType == null || !report.ReportType.Equals("delivery-status", StringComparison.OrdinalIgnoreCase))
                return null;

            string? IsError = null;
            report.OfType<MessageDeliveryStatus>().ToList().ForEach(x => {
                x.StatusGroups.Where(y => y.Contains("Action") && y.Contains("Final-Recipient")).ToList().ForEach(z => {
                    switch (z["Action"])
                    {
                        case "failed":
                            IsError = $"Delivery of message {z["Action"]} failed for {z["Final - Recipient"]}";
                            break;
                        case "delayed":
                            IsError = $"Delivery of message {z["Action"]} has been delayed for {z["Final-Recipient"]}";
                            break;
                        case "delivered":
                            IsError = $"Delivery of message {z["Action"]} has been delivered to {z["Final-Recipient"]}";
                            break;
                        case "relayed":
                            IsError = $"Delivery of message {z["Action"]} has been relayed for {z["Final-Recipient"]}";
                            break;
                        case "expanded":
                            IsError = $"Delivery of message {z["Action"]} has been delivered to {z["Final-Recipient"]} and relayed to the the expanded recipients";
                            break;
                    }
                });
            });
            return IsError;
        }
    }
}
