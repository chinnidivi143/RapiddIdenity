using System.Net.Mail;

namespace RapiddIdenity.Services
{
    public class MailDetails
    {
        //public string From { get; set; }
        public string To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<string> CC { get; set; }
        public List<string> BBC { get; set; }
       // public MailMessage msg = new();
    }
}
