using Kaizen.Core.Defines;
using NUnit.Framework;

namespace Core.Test.Defines
{
    [TestFixture]
    public class TimeConstantsTest
    {
        [Test]
        public void CheckTimeConstantsValues()
        {
            Assert.AreEqual(60, TimeConstants.Seconds);
            Assert.AreEqual(1000, TimeConstants.Milliseconds);
            Assert.AreEqual(60, TimeConstants.Minutes);
        }
    }
}
