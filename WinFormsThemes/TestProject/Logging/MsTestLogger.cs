using Microsoft.Extensions.Logging;
using System.Text;

namespace TestProject.Logging
{
    /// <summary>
    /// Logger implementation for writing to the test context
    /// </summary>
    internal class MsTestLogger : ILogger
    {
        /// <summary>
        /// used to support <see cref="BeginScope{TState}(TState)"/>
        /// </summary>
        private readonly IExternalScopeProvider _scopeProvider = new LoggerExternalScopeProvider();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="categoryName">the category name</param>
        /// <param name="testContext">the test context</param>
        public MsTestLogger(string categoryName, TestContext testContext)
        {
            CategoryName = categoryName;
            TestContext = testContext;
        }

        /// <summary>
        /// the current category name
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// the current test context
        /// </summary>
        public TestContext TestContext { get; }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return _scopeProvider.Push(state);
        }

        /// <summary>
        /// for test purposes show all logs
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = formatter(state, exception);
            StringBuilder scopeBuilder = new StringBuilder("=> ");
            _scopeProvider.ForEachScope((scope, state) =>
            {
                scopeBuilder.Append(scope);
                scopeBuilder.Append(", ");
            }, state);

            TestContext.WriteLine($"{DateTime.Now} - {CategoryName} ({eventId}) {scopeBuilder} -  {logLevel}: {message}");
        }
    }
}