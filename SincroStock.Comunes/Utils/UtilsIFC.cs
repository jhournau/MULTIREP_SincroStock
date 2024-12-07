using GC.Utils.EMail;
using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Utils
{
    public static class UtilsIFC
    {
        private static ILog logger = LogManager.GetLogger(typeof(UtilsIFC));

        public static void EnviarMailNotificacion(string asunto, string mensaje, string destinatarios, 
            bool esHtml = false, int timeoutEnMs = 60000)
        {
            ConfigGeneral config = ConfigGeneral.Instance;

            LogUtil.Log(logger, Level.Debug, $"Enviando e-mail de notificación.{Environment.NewLine}Destinatario(s): {destinatarios}{Environment.NewLine}Asunto: {asunto ?? ""}{Environment.NewLine}Mensaje: {mensaje ?? ""}", true, false);
            
            EmailConfig emailConfig = new EmailConfig(config.DireccionMail, config.ServidorMail, config.UsuarioMail, config.PasswordMail,
            config.PuertoMail, false, config.UsaSSL);
            emailConfig.EmailUsarSSLImplicito = false;
            Email email = new Email(config.DireccionMail, config.DireccionMail, destinatarios, "", "", asunto, mensaje, "", esHtml);
            UtilsIFC.sendMailTo(emailConfig, email, timeoutEnMs);
        }

        public static void sendMailTo(EmailConfig emailConfig, Email email, int timeoutEnMS = 20000)
        {

            MailMessage correo = email.getMailMessage(emailConfig.EmailDireccion);

            SmtpClient smtp = new SmtpClient();

            smtp.Host = emailConfig.EmailHost;
            smtp.Port = emailConfig.EmailPuerto;
            smtp.UseDefaultCredentials = emailConfig.EmailUsarCredencialesDefault;
            if (!emailConfig.EmailUsarCredencialesDefault)
                smtp.Credentials = new NetworkCredential(emailConfig.EmailUser, emailConfig.EmailPassword);
            smtp.EnableSsl = emailConfig.EmailUsarSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Timeout = timeoutEnMS;

            try
            {
                smtp.Send(correo);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
