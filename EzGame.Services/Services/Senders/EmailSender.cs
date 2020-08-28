using EzGame.Common.ViewModel.Settings;
using EzGame.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MPS.Services.Services.Senders
{
    public class EmailSender : ISender
    {

        private readonly IOptionsSnapshot<SiteSettings> _emailSettings;
        public EmailSender(IOptionsSnapshot<SiteSettings> emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public async Task<bool> SendManyAsync(IEnumerable<string> to, string subject, string message, bool isMessageHtml = false)
        {
            try
            {
                foreach (var email in to)
                {
                    await SendEmail(email, subject, message, isMessageHtml).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> SendEmailAsync(string to, string subject, string message, bool isMessageHtml = false)
        {
            try
            {
                await SendEmail(to, subject, message, isMessageHtml);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddedEmailAsync(string to)
        {
            const string subject = "ایزی گیم";
            const string message = "ایمیل شما با موفقیت در خبر نامه سایت ثبت شده است";
            try
            {
                await SendEmail(to, subject, message, false);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> SendEmail(string to, string subject, string message, bool isMessageHtml = false)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    var email = _emailSettings.Value.EmailSetting.Email;
                    var username = _emailSettings.Value.EmailSetting.Username;
                    var passWord = _emailSettings.Value.EmailSetting.Password;
                    var host = _emailSettings.Value.EmailSetting.Host;
                    var port = _emailSettings.Value.EmailSetting.Port;

                    var credentials = new NetworkCredential()
                    {
                        UserName = username, // without @gmail.com
                        Password = passWord
                    };
                    client.UseDefaultCredentials = true;
                    client.Credentials = credentials;
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;

                    using var emailMessage = new MailMessage()
                    {
                        To = { new MailAddress(to) },
                        From = new MailAddress(email), // with @gmail.com
                        Subject = subject,
                        Body = message,
                        IsBodyHtml = isMessageHtml
                    };

                    await client.SendMailAsync(emailMessage).ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}