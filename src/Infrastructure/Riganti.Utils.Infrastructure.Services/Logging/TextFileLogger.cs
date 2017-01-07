﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using Riganti.Utils.Infrastructure.Core;

namespace Riganti.Utils.Infrastructure.Services.Logging
{
    public class TextFileLogger : LoggerBase
    {
        private readonly string directory;


        public TextFileLogger(string directory, IDateTimeProvider dateTimeProvider, IEnumerable<IAdditionalDataProvider> additionalDataProviders = null) : base(dateTimeProvider, additionalDataProviders)
        {
            this.directory = directory;

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        protected virtual string GetLogFileName()
        {
            return Path.Combine(directory, dateTimeProvider.Now.ToString("yyyy-MM-dd") + ".txt");
        }

        protected override void LogMessageCore(string message, IDictionary<string, string> additionalData, Severity severity)
        {
            var output = $"{dateTimeProvider.Now:yyyy-MM-dd HH:mm:ss}\t{message}\r\n\r\n";
            File.AppendAllText(GetLogFileName(), output, Encoding.UTF8);
        }
    }
}
