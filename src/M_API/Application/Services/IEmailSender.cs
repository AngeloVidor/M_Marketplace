using M_API.Application.DTOs;

namespace Application.Services
{
    public interface IEmailSender
    {
        Task SendAsync(EmailMessage message);
    }
}
