using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileWatcher
{
    class Logger
    {
        public static LogOptions loggerOptions = new LogOptions();
        static string preLog = "";

        public static bool loaded = false;


        public static void Log(string msg)
        {

            if (!loaded)
            {
                preLog += $"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {msg}\n";
                return;
            }

            if (loggerOptions.NeedToLog)
            {
                string logDir = Path.GetDirectoryName(loggerOptions.LogFile);

                try
                {
                    if (!Directory.Exists(logDir))
                    {
                        Directory.CreateDirectory(logDir);
                    }
                    if (!File.Exists(loggerOptions.LogFile))
                    {
                        File.Create(loggerOptions.LogFile).Close();
                    }
                }
                catch
                {
                    loggerOptions.NeedToLog = false;
                    return;
                }

                if (preLog != "")
                {
                    using (StreamWriter sw = new StreamWriter(loggerOptions.LogFile, true))
                    {
                        sw.Write($"{preLog}");
                    }
                    preLog = "";
                }

                using (StreamWriter sw = new StreamWriter(loggerOptions.LogFile, true))
                {
                    sw.WriteLine($"[{DateTime.Now:hh:mm:ss dd.MM.yyyy}] - {msg}");
                }
            }
        }
    }
}
