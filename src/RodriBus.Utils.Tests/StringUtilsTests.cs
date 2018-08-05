using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Rodribus.Utils;
using Xunit;

namespace RodriBus.Utils.Tests
{
    public static class StringUtilsTests
    {
        [Fact]
        public static void ShouldEncode()
        {
            var plainText = "Some text with ñçá characters.";
            var expectedEncoded = "U29tZSB0ZXh0IHdpdGggw7HDp8OhIGNoYXJhY3RlcnMu";

            var actualEncoded = StringUtils.Base64Encode(plainText);

            actualEncoded.Should().Be(expectedEncoded);
        }

        [Fact]
        public static void ShouldDecode()
        {
            var decoded = "U29tZSB0ZXh0IHdpdGggw7HDp8OhIGNoYXJhY3RlcnMu";
            var expectedPlainText= "Some text with ñçá characters.";

            var actualPlainText = StringUtils.Base64Decode(decoded);

            actualPlainText.Should().Be(expectedPlainText);
        }
    }
}
