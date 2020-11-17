using NUnit.Framework;
using Tnt.CoreLib.Functional;

namespace Tnt.CoreLib.Functional.Tests
{
    public class UnionTests
    {
        [Test]
        public void MatchUnion2() 
        {
            var union = Union.Of<int, bool>(true);
            union.Match(x => Assert.Fail(), x => {});
            union = Union.Of<int, bool>(1);
            union.Match(x => {}, x => Assert.Fail());
        }

        [Test]
        public void MatchUnion3() 
        {
            var union = Union.Of<int, bool, string>(true);
            union.Match(x => Assert.Fail(), x => {}, x => Assert.Fail());
            union = Union.Of<int, bool, string>("foo");
            union.Match(x => Assert.Fail(), x => Assert.Fail(), x => {});
        }

        [Test]
        public void MatchUnion4() 
        {
            var union = Union.Of<int, bool, char, string>('c');
            union.Match(x => Assert.Fail(), x => Assert.Fail(), x => {}, x => Assert.Fail());
            union = Union.Of<int, bool, char, string>(1);
            union.Match(x => {}, x => Assert.Fail(), x => Assert.Fail(), x => Assert.Fail());
        }

        [Test]
        public void UnionEquals()
        {
            var left = Union.Of<int, bool>(0);
            var right = Union.Of<int, bool>(0);
            Assert.True(left == right);
            Assert.False(left != right);

            left = Union.Of<int, bool>(1);
            Assert.False(left == right);
            
            var left2 = Union.Of<bool, int>(0);
            Assert.False(left2 == right);
        }
    }
}