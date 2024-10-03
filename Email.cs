using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KioskUpdater
{
    public class Email
    {
        public void SendERPExceptionEmail(string RequestErrorMessage)
        {
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["SendExceptionEmails"]))
                {
                    var Subject = "ERP Daily Exceptions (Kiosk)";
                    var Body = "--------------------------<br/>" + "RequestErrorMessage : - <br/>" + RequestErrorMessage.ToString() + "<br/>--------------------------<br/>";
                    string[] SendTo = ConfigurationManager.AppSettings["ExceptionSendToEmail"].ToString().Split(',');
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);

                        mailMessage.Subject = Subject;

                        mailMessage.Body = Body;

                        mailMessage.IsBodyHtml = true;

                        mailMessage.To.Add(new MailAddress(SendTo[0]));

                        for (int i = 1; i < SendTo.Length; i++)
                        {
                            mailMessage.CC.Add(new MailAddress(SendTo[i]));
                        }

                        SmtpClient smtp = new SmtpClient();

                        smtp.Host = ConfigurationManager.AppSettings["Host"];

                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                        NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];

                        NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
                        smtp.UseDefaultCredentials = true;

                        smtp.Credentials = NetworkCred;

                        smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                        smtp.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
