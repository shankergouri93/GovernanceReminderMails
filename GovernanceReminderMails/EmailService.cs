using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GovernanceReminderMails
{
    public class EmailService
    {
        public EmailService()
        {

        }

        public async Task SendEmail(SMTP smtp)
        {
            try
            {


                string subject = string.Format("{0} [GOVERNANCE REMINDER] - REMIDIATION DATE - {1}", smtp.Status, smtp.RITM);
                string bodyhtml = File.ReadAllText("emailbody.html");
                string body = string.Format(bodyhtml, "",
                    smtp.RITM,
                    smtp.Status,
                    smtp.NoOfInterfaces,
                    smtp.Task,
                    smtp.RequestSummary,
                    smtp.InterfaceID,
                    smtp.AgreedRemediation,
                    smtp.NextleadershipcallDate,
                    smtp.Notes);


                var fromAddress = new MailAddress("oersted.kube3@gmail.com", "Oersted");
                var toAddress = new MailAddress(smtp.EmailId, "Jagadish");
                const string fromPassword = "Hallmark@1";

                var smtpclient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtpclient.SendMailAsync(message);
                }
            }
            catch
            {
            }
        }
    }
}
