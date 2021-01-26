using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabloid_Fullstack.Tests.PostTests
{
    public class PostControllerTests : EFTestFixture
    {
        public PostControllerTests()
        {
            // When constructed, generate dummy data, passing in EFTestFixture database
            PostDummyData.GenerateData(_context);
        }
    }
}
