using Microsoft.Extensions.Logging;
using TestProject.Logging;

namespace TestProject
{
    /// <summary>
    /// Base class for Tests providing access to the logs
    /// </summary>
    public abstract class AbstractTestClass
    {
        /// <summary>
        /// the current Test context
        /// </summary>
        public TestContext? TestContext { get; set; }

        /// <summary>
        /// Logging factory
        /// </summary>
        protected ILoggerFactory LoggerFactory { get; private set; }

        /// <summary>
        /// cleanup logging
        /// </summary>
        [TestCleanup]
        public void CleanupLogging()
        {
            LoggerFactory?.Dispose();
        }

        /// <summary>
        /// Initialize logging
        /// </summary>
        [TestInitialize]
        public void SetupLogging()
        {
            LoggerFactory = new LoggerFactory(new[] { new MsTestLoggerProvider(TestContext) });
        }
    }
}