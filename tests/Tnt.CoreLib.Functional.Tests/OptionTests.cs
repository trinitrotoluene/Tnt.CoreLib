using System;
using NUnit.Framework;
using Tnt.CoreLib.Functional;

namespace Tnt.CoreLib.Functional.Tests
{
    public class OptionTests
    {
        [Test]
        public void TestSpecified()
        {
            var option = Option.Some(1);
            Assert.AreEqual(1, option.Value);
            Assert.True(option.IsSpecified);

            Assert.AreEqual(1, (int)option);
        }

        [Test]
        public void TestUnspecified()
        {
            var option = Option.None<int>();
            Assert.Throws<InvalidOperationException>(() => { _ = option.Value; });
            Assert.Throws<InvalidOperationException>(() => { _ = (int)option; });
            Assert.False(option.IsSpecified);
        }

        [Test]
        public void TestEquality()
        {
            var optionA = Option.Some(1);
            var optionB = Option.Some(1);
            var optionC = Option.None<int>();

            Assert.True(optionA == optionB);
            Assert.True(optionA.Equals(optionB));
            Assert.False(optionA == optionC);
            Assert.False(optionA.Equals(optionC));
            Assert.True(optionA == 1);
            Assert.True(optionA.Equals(1));
            Assert.False(optionC == 1);
            Assert.False(optionC.Equals(1));
        }
    }
}