using Microsoft.Extensions.Logging;
using TestProject.Logging;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
        public TestContext TestContext { get; set; }

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
#pragma warning restore CS8618