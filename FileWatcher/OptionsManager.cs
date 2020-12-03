using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileWatcher
{
    class OptionsManager
    {
        ETLOptions defaultOptions;
        ETLJSONOptions jsonOptions;
        ETLXMLOptions xmlOptions;


        bool isJsonConfigured = false;
        bool isXmlConfigured = false;

        public OptionsManager(string path)
        {
            string options;

            try
            {
                using (StreamReader sr = new StreamReader($"{path}\\appsettings.json"))
                {
                    options = sr.ReadToEnd();
                }

                jsonOptions = new ETLJSONOptions(options);
                isJsonConfigured = true;
                Logger.Log("appsettings.json is loaded.");
            }
            catch (Exception ex)
            {
                isJsonConfigured = false;
                Logger.Log(ex.Message);
            }

            try
            {
                using (StreamReader sr = new StreamReader($"{path}\\config.xml"))
                {
                    options = sr.ReadToEnd();
                }

                xmlOptions = new ETLXMLOptions(options);
                isXmlConfigured = true;
                Logger.Log("config.xml is loaded.");
            }
            catch (Exception ex)
            {
                isXmlConfigured = false;
                Logger.Log(ex.Message);
            }

            if (!isJsonConfigured && !isXmlConfigured)
            {
                defaultOptions = new ETLOptions();
                Logger.Log("Default options is used.");
            }
        }

        Options FindOption<T>(ETLOptions options)
        {
            if (typeof(T) == typeof(ETLOptions))
            {
                return options;
            }

            try
            {
                return options.GetType().GetProperty(typeof(T).Name).GetValue(options, null) as Options;
            }
            catch
            {
                Logger.Log("FindOption didn't find the needed option and throw a NotImplementedException.");
                throw new NotImplementedException();
            }
        }

        public Options GetOptions<T>()
        {
            if (isJsonConfigured)
            {
                Logger.Log("Json configuration");
                return FindOption<T>(jsonOptions);
            }
            else if (isXmlConfigured)
            {
                Logger.Log("XML configuration");
                return FindOption<T>(xmlOptions);
            }
            else
            {
                Logger.Log("Default configuration");
                return FindOption<T>(defaultOptions);
            }
        }
    }
}
