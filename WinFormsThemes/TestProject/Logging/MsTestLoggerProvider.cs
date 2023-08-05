using Microsoft.Extensions.Logging;

namespace TestProject.Logging
{
    /// <summary>
    /// provider for generating logs that write to the test context
    /// </summary>
    internal class MsTestLoggerProvider : ILoggerProvider
    {
        /// <summary>
        /// the current test context
        /// </summary>
        private readonly TestContext _testContext;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="testContext">the current test context</param>
        public MsTestLoggerProvider(TestContext testContext)
        {
            _testContext = testContext;
        }

        /// <summary>
        /// return a logger for a given category name
        /// </summary>
        /// <param name="categoryName">ther category name</param>
        /// <returns></returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new MsTestLogger(categoryName, _testContext);
        }

        /// <summary>
        /// Dispose - empty implementation
        /// </summary>
        public void Dispose()
        { }
    }
}