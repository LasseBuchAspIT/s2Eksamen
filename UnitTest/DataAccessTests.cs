using DataAccess;
using Entities;

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

        [Fact]
        public void CanGetBookers()
        {
            Repository repo;

            repo = new();
            List<Booker> bookers = new List<Booker>();
            bookers = repo.GetallBookers();

            Assert.NotEmpty(bookers);

        }

        [Fact]
        public void CanGetPitches()
        {
            Repository repo = new();
            List<Pitch> pitches = new List<Pitch>();

            pitches = repo.GetAllPitches();

            Assert.NotEmpty(pitches);

        }

    }
}