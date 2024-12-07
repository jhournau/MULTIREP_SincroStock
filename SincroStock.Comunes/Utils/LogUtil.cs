using GC.Utils.Logger;
using log4net;
using log4net.Core;
using log4net.Extensions;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincroStock.Comunes.Utils
{
    public static class LogUtil
    {

        public static void Log(ILog logger, Level logLevel, string message, bool sendLoggerPrincipal, bool sendGUI, Exception ex = null)
        {
            ConfigGeneral cfg = ConfigGeneral.Instance;
            ILogger loggerMailObject = null,
                    loggerGuiObject = null,
                    loggerObject = logger.Logger;

            if (sendLoggerPrincipal && loggerObject.IsEnabledFor(logLevel))
                loggerObject.Log(logger.GetType(), logLevel, message, ex);

            if (sendGUI && cfg.SendGuiLog)
            {
                loggerGuiObject = LogManager.GetLogger(cfg.GuiLoggerName).Logger;
                if (loggerGuiObject.IsEnabledFor(logLevel))
                    loggerGuiObject.Log(logger.GetType(), logLevel, message, null);
            }
            //if (sendMailVentas && cfg.SendEmailLog)
            //{
            //    loggerMailObject = LogManager.GetLogger(cfg.EmailLoggerName).Logger;
            //    if (loggerMailObject.IsEnabledFor(logLevel))
            //        loggerMailObject.Log(logger.GetType(), logLevel, message, null);
            //}
            //if (sendMailCierre && cfg.SendEmailLog)
            //{
            //    loggerMailObject = LogManager.GetLogger(cfg.EmailLoggerNameCierre).Logger;
            //    if (loggerMailObject.IsEnabledFor(logLevel))
            //        loggerMailObject.Log(logger.GetType(), logLevel, message, null);
            //}
            //if (sendMailFacturacion && cfg.SendEmailLog)
            //{
            //    loggerMailObject = LogManager.GetLogger(cfg.EmailLoggerNameFacturas).Logger;
            //    if (loggerMailObject.IsEnabledFor(logLevel))
            //        loggerMailObject.Log(logger.GetType(), logLevel, message, null);
            //}
        }

        //public static void LogWithMail(ILog logger, Level logLevel, string message, bool sendMailFacturas, Exception ex = null)
        //{
        //    LogUtil.Log(logger, logLevel, message, true, sendMailFacturas, false, ex);
        //}

        public static void LogWithGui(ILog logger, Level logLevel, string message, bool sendGUI, Exception ex = null)
        {
            LogUtil.Log(logger, logLevel, message, true, sendGUI, ex);
        }

        public static void Log(ILog logger, Level logLevel, string message, Exception ex = null)
        {
            LogUtil.Log(logger, logLevel, message, true, false, ex);
        }

        //public static void FlushMail()
        //{
        //    ConfigGeneral cfg = ConfigGeneral.Instance;
        //    Logger logger = (Logger)LogManager.GetLogger(cfg.EmailLoggerName).Logger;
        //    foreach (SmtpCachingAppender smtpCachingAppender in logger.Appenders.ToArray().ToList().FindAll(a => a is SmtpCachingAppender))
        //        smtpCachingAppender.FlushBuffer();
        //}

        public static void SetupLog(
                string rutaArchivoLog,
                bool detallado,
                uint maxFileSizeMB,
                string guiLoggerName,
                RichTextBox rTextBox,
                List<EmailLoggerInfo> emailLoggersInfo)
        {
            try
            {
                log4net.Repository.Hierarchy.Logger emailLogger, guiLogger;
                LogHelper.InitializeBasicLog(rutaArchivoLog, detallado, maxFileSizeMB);
                SmtpCachingAppender appender;

                Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
                if (emailLoggersInfo != null)
                {
                    foreach (EmailLoggerInfo emailLoggerInfo in emailLoggersInfo)
                    {
                        emailLogger = (log4net.Repository.Hierarchy.Logger)LogManager.GetLogger(emailLoggerInfo.LoggerName).Logger;
                        foreach (SmtpCachingAppender smtpCachingAppender in emailLogger.Appenders.ToArray().ToList().FindAll(a => a is SmtpCachingAppender))
                        {
                            smtpCachingAppender.FlushBuffer();
                            smtpCachingAppender.Close();
                            emailLogger.RemoveAppender(smtpCachingAppender);
                        }
                        emailLogger.RemoveAllAppenders();
                        emailLogger.Additivity = emailLoggerInfo.LoggerAdditivity;
                        appender = LogHelper.GetEmailCachingAppender(emailLoggerInfo.EmailConfig, emailLoggerInfo.IntervaloEnvioEnMinutos, emailLoggerInfo.CantMaximaMensajesErrorParaEnvio, 0, emailLoggerInfo.EnvioActivado);
                        appender.ManualFlush = emailLoggerInfo.ManualFlush;
                        emailLogger.AddAppender(appender);
                    }
                }

                guiLogger = (log4net.Repository.Hierarchy.Logger)LogManager.GetLogger(guiLoggerName).Logger;
                guiLogger.RemoveAllAppenders();
                guiLogger.Additivity = false;
                if (rTextBox != null)
                    guiLogger.AddAppender(LogHelper.GetGUIAppender(rTextBox));
            }
            catch (Exception ex)
            {
                throw new LogException("Error al configurar log: " + ex.Message, ex);
            }

        }
    }
}
