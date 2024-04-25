using System.Threading.Tasks;

namespace MVC_Project_Presentation_Layer.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendAsync(string from , string recipients , string subject , string body );
    }
}
