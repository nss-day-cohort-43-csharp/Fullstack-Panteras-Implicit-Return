// Repository Unit Tests by Sam Edwards
using System;
using Xunit;

// We are able to do these tests because we "spoof" the ApplicationDbContext using Entity Framework and the EFTextFixture Class
// For each test, we create a new database in memory. Add some seed data, run our test, then delete Db.
// This way we have total control of our test environment.
namespace Tabloid_Fullstack.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
