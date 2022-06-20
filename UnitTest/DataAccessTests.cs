using DataAccess;

namespace UnitTest
{
    public class DataAccessTests
    {
        [Fact]
        public void CanConnectToRepo()
        {
            Repository repo;

            repo = new();

            Assert.NotNull(repo);
        }

    }
}