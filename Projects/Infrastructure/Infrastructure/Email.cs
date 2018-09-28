using System.Net.Mail;
using System.Text;

namespace Infrastructure
{
    public static class Email
    {
        public static void Send()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("esteembamexport@gmail.com", "3st33m2020!");

            var subject = "BAMExport - Data Exported - Esteem to BAM System Exception";
            var body = "There has been an exception in the System. The following check the Esteem website to see which items have failed. http://172.16.10.81:8080/ServiceProgressReports";

            MailMessage mail = new MailMessage("esteembamexport@gmail.com", "d.bowditch@esteem.co.uk, h.proctor@esteem.co.uk", subject, body);

            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mail);
        }

        public static void Send(int reportId)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("esteembamexport@gmail.com", "3st33m2020!");

            var subject = "BAMExport - Data Exported - Esteem to BAM System Exception";
            var body = "There has been an exception in the System. The following check the Esteem website to see which items have failed. \r\n" +
                " http://172.16.10.81:8080/BAM_Reporting/BAM_HardwareTemplate_From_Report/" + reportId;

            MailMessage mail = new MailMessage("esteembamexport@gmail.com", "d.bowditch@esteem.co.uk, h.proctor@esteem.co.uk", subject, body);

            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mail);
        }


        public static void SendException(string exceptionMessage)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.Credentials = new System.Net.NetworkCredential("esteembamexport@gmail.com", "3st33m2020!");

            var subject = "Esteem to BAM System Error";
            var body = "There has been an exception in the System \r\n \r\n" + exceptionMessage;

            MailMessage mail = new MailMessage("esteembamexport@gmail.com", "d.bowditch@esteem.co.uk, h.proctor@esteem.co.uk", subject, body);

            mail.BodyEncoding = UTF8Encoding.UTF8;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mail);
        }

    }
}
