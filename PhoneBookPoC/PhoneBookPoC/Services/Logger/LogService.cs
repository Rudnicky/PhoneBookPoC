using NLog;
using NLog.Config;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PhoneBookPoC.Services
{
    public class LogService : ILogService
    {
        private Logger _logger;

        public void Initialize(Assembly assembly, string assemblyName)
        {
            var loc = GetEmbeddedResourceStream(assembly, "NLog.config");
            var xmlReader = System.Xml.XmlReader.Create(loc);
            LogManager.Configuration = new XmlLoggingConfiguration(xmlReader, null);
            this._logger = LogManager.GetCurrentClassLogger();
        }

        public void LogDebug(string message)
        {
            this._logger.Info(message);
        }

        public void LogError(string message)
        {
            this._logger.Error(message);
        }

        public void LogFatal(string message)
        {
            this._logger.Fatal(message);
        }

        public void LogInfo(string message)
        {
            this._logger.Info(message);
        }

        public void LogWarning(string message)
        {
            this._logger.Warn(message);
        }
        private Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourcePaths = assembly.GetManifestResourceNames()
              .Where(x => x.EndsWith(resourceFileName, StringComparison.OrdinalIgnoreCase))
              .ToList();
            if (resourcePaths.Count == 1)
            {
                return assembly.GetManifestResourceStream(resourcePaths.Single());
            }
            return null;
        }
    }
}
