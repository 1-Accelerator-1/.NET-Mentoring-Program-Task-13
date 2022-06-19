using IntegrationTests.ConnectionHelpers;
using NUnit.Framework;

namespace IntegrationTests
{
    public class TestBase
    {
        protected string ConnectionString { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ConnectionString = ConnectionHelper.GetConnnectionString();
        }
    }
}
