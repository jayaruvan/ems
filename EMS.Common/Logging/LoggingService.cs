using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



using System.IO;
using System.Reflection;
using System.Configuration;

using log4net;
using log4net.Repository.Hierarchy;
using log4net.Core;
using log4net.Appender;
using log4net.Layout;


using System.Diagnostics;
using System.Globalization;


namespace EMS.Common.Logging
{
    // For the time being this is okay
    //public sealed class LoggingService :ILogger
            
   public sealed class LoggingService :ILogger
        //: ILoggingService
    {
        private static readonly ILog log = LogManager.GetLogger (MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly ILogger logger = null;
        private static ILogger instance = null;
        private static readonly object objLock = new object();
        private static Level logLevel = getLevel();
        //Has to move to Config ...
        private static string logDir = ConfigurationSettings.AppSettings["LogDirectory"];
        private static string logFile = String.Concat(logDir, @"\", Config.LogFileName, logLevel, "_", DateTime.Now.ToString("ddmmyyyy"), ".log");


        private static Level getLevel()
        {
              Level logLevel = Level.Error; //default value  
              switch (ConfigurationSettings.AppSettings["LogLevel"].ToUpper())
                {
                    case "ALL":
                        logLevel = Level.All;
                        break;
                    case "OFF":
                        logLevel = Level.Off;
                        break;
                    case "DEBUG":
                        logLevel = Level.Debug;
                        break;
                    case "INFO":
                        logLevel = Level.Info;
                        break;
                    case "WARN":
                        logLevel = Level.Warn;
                        break;
                    case "ERROR":
                        logLevel = Level.Error;
                        break;
                    case "FATAL":
                        logLevel = Level.Fatal;
                        break;

                    default:
                        logLevel=Level.Error;
                        break;
                }
                return logLevel;
        }

        private LoggingService()
        {
            //string path = Directory.GetCurrentDirectory();
            string path = AppDomain.CurrentDomain.BaseDirectory.ToString();//.BaseDirectory();
            string target = string.Concat(path, @"\", logDir);
            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }

            LoggingService.setup();
            
        }

        public static ILogger GetLog()
        {
            lock (objLock)
            {
                if (instance == null)
                {
                    instance = new LoggingService();
                }
                return instance;;
            }            
        }

        private static void setup()
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date   [%thread] %-5level %logger - %message%newline";

            
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.AppendToFile = true;
            roller.File = logFile;
            roller.Layout = patternLayout;
            roller.MaxSizeRollBackups = 5;
            roller.MaximumFileSize = "100Mb";
            roller.RollingStyle = RollingFileAppender.RollingMode.Size;
            roller.StaticLogFileName = true;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            MemoryAppender memory = new MemoryAppender();
            memory.ActivateOptions();
            hierarchy.Root.AddAppender(memory);

            hierarchy.Root.Level = logLevel;
            hierarchy.Configured = true;
        }


        public void Log(string message)
        {
            log.Info(message);
            
        }

        #region ILogger Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogInfo(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                log.Info(messageToTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogWarning(string message, params object[] args)
        {

            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                log.Warn(messageToTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void LogError(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

               log.Error( messageToTrace);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public void LogError(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                log.Error(string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Debug(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                log.Debug(messageToTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public void Debug(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                log.Debug(string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Debug(object item)
        {
            if (item != null)
            {
                log.Debug(item.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Error(object item)
        {
            if (item != null)
            {
                log.Error(item.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Fatal(string message, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message))
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                log.Fatal(messageToTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="args"></param>
        public void Fatal(string message, Exception exception, params object[] args)
        {
            if (!String.IsNullOrWhiteSpace(message)
                &&
                exception != null)
            {
                var messageToTrace = string.Format(CultureInfo.InvariantCulture, message, args);

                var exceptionData = exception.ToString(); // The ToString() create a string representation of the current exception

                log.Fatal(string.Format(CultureInfo.InvariantCulture, "{0} Exception:{1}", messageToTrace, exceptionData));
            }
        }


        #endregion

    }


}