namespace HelloWeb.Loggers
{
    public class FileLogger : ILogger, IDisposable
    {
        string filePath;
        static object _lock = new object();
        public FileLogger(string filePath)
        {
            this.filePath = filePath;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
        public void Dispose() { }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception? exception,
            Func<TState, Exception?, string> formatter)
        {
            lock(_lock)
            {
                if (logLevel == LogLevel.Error)
                {
                    File.AppendAllText(filePath, $"{logLevel}: {formatter(state, exception)}" + Environment.NewLine);
                }
            }
        }    
    }
}
