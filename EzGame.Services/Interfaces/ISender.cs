using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EzGame.Services.Interfaces
{
    public interface ISender
    {
        Task<bool> SendManyAsync(IEnumerable<string> to , string subject, string message, bool isMessageHtml = false);
        Task<bool> SendEmailAsync(string to, string subject, string message, bool isMessageHtml = false);
        Task<bool> AddedEmailAsync(string to);
    }
}